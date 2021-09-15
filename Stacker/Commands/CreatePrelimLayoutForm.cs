using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Structure;
using Stacker.Commands;
using System.IO;
using Stacker.ModClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Stacker.Commands
{
    public partial class CreatePrelimLayoutForm : System.Windows.Forms.Form
    {
        public double FloorOverallLength;
        public double FloorOverallWidth;
        public double FloorHallwayWidth;
        public double FloorOverallSquareFootage;

        public double PodLengthMin;
        public double PodLengthMax;
        public double PodWidthMin;
        public double PodWidthMax;

        public int PriorityStudio;
        public int Priority1Bed;
        public int Priority2Bed;

        public double PercentageStudio;
        public double Percentage1Bed;
        public double Percentage2Bed;



        private Document _doc;
        private UIDocument _uidoc;

        List<FloorLayout> FloorLayoutOptions { get; set; }

        Dictionary<string, string> DictJSON;

        public bool formClosed = false;


        public bool LevelBuilt = false;
        public Dictionary<string, List<ElementId>> ElementsBuilt = new Dictionary<string, List<ElementId>>();

        public Level Level = null;
        public ViewPlan VPlan = null;

        public List<Level> allLevels = new List<Level>();
        public List<ViewPlan> allViewPlans = new List<ViewPlan>();

        double TypFloorHeight = 12.0;
        int TotalFloors = 1;

        public CreatePrelimLayoutForm(Document doc, UIDocument uidoc)
        {
            InitializeComponent();

            _doc = doc;
            _uidoc = uidoc;

            PodLengthMin = 20;
            PodLengthMax = 35;
            PodWidthMin = 10;
            PodWidthMax = 16;

            tbModLengthMin.Text = PodLengthMin.ToString();
            tbModLengthMax.Text = PodLengthMax.ToString();
            tbModWidthMin.Text = PodWidthMin.ToString();
            tbModWidthMax.Text = PodWidthMax.ToString();

            FloorLayoutOptions = new List<FloorLayout>();
            DictJSON = new Dictionary<string, string>();

            cbOptionsStudio.Items.Add("ModLab_BD_0_MOD_1_TYPA");
            cbOptionsStudio.Items.Add("ModLab_BD_0_MOD_1_TYPS1");
            cbOptionsStudio.Items.Add("ModLab_BD_0_MOD_1_TYPS2");
            cbOptionsStudio.Items.Add("ModLab_BD_0_MOD_1_TYPS3");

            cbOptions1Bed.Items.Add("ModLab_BD_1_MOD_2_TYPA");

            cbOptions2Bed.Items.Add("ModLab_BD_2_MOD_3_TYPA");
            cbOptions2Bed.Items.Add("ModLab_BD_2_MOD_3_TYPB1");
            cbOptions2Bed.Items.Add("ModLab_BD_2_MOD_3_TYPB2");
            cbOptions2Bed.Items.Add("ModLab_BD_2_MOD_3_TYPB3");

            double flrHeight = Convert.ToDouble(tbTypStoryHeight.Text);
            double bldgHeight = Convert.ToDouble(tbTotalBuildingHeight.Text);

            string maxFloors = Convert.ToString(calculateTotalFloors(flrHeight, bldgHeight));
            tbMaxFloors.Text = maxFloors;
            tbFloorsTotal.Text = maxFloors;

            TypFloorHeight = flrHeight;

        }



        /// <summary>
        /// Set Initial Geometry Parameters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplyFlrDim_Click(object sender, EventArgs e)
        {
            FloorOverallLength = Convert.ToDouble(tbLength.Text);
            FloorOverallWidth = Convert.ToDouble(tbWidth.Text);
            FloorHallwayWidth = Convert.ToDouble(tbHallwayWidth.Text);

            FloorOverallSquareFootage = FloorOverallLength * FloorOverallWidth;

            tbTotalSquareFootage.Text = FloorOverallSquareFootage.ToString();
        }







        private void btnDeleteGeom_Click(object sender, EventArgs e)
        {

            if (ElementsBuilt.Count > 0)
            {
                using (Transaction transDeleteOldMods = new Transaction(_doc, "Mod: Delete Old Mods"))
                {
                    transDeleteOldMods.Start();

                    foreach (var elemCat in ElementsBuilt)
                    {
                        foreach (var elem in elemCat.Value)
                        {
                            Element searchElem = _doc.GetElement(elem);

                            if (searchElem != null)
                                _doc.Delete(elem);
                        }
                    }

                    ElementsBuilt = new Dictionary<string, List<ElementId>>();

                    transDeleteOldMods.Commit();
                }
            }
        }





        /// <summary>
        /// Entire process of building floor layouts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuildLayout_Click(object sender, EventArgs e)
        {
            try
            {
                FloorOverallLength = Convert.ToDouble(tbLength.Text);
                FloorOverallWidth = Convert.ToDouble(tbWidth.Text);
                FloorHallwayWidth = Convert.ToDouble(tbHallwayWidth.Text);

                PercentageStudio = Convert.ToDouble(tbPercentageStudio.Text);
                Percentage1Bed = Convert.ToDouble(tbPercentage1Bed.Text);
                Percentage2Bed = Convert.ToDouble(tbPercentage2Bed.Text);

                PodLengthMin = Convert.ToDouble(tbModLengthMin.Text);
                PodLengthMax = Convert.ToDouble(tbModLengthMax.Text);
                PodWidthMin = Convert.ToDouble(tbModWidthMin.Text);
                PodWidthMax = Convert.ToDouble(tbModWidthMax.Text);

                TotalFloors = Convert.ToInt32(tbFloorsTotal.Text);

                double maxFloorLength = Convert.ToDouble(tbLength.Text) + 50;


                double totalSF = FloorOverallSquareFootage;
                double totalSFForUnits = FloorOverallSquareFootage - (FloorHallwayWidth * FloorOverallLength);


                double fixedModWidth = Convert.ToDouble(tbFixedWidth.Text);



                // Begin to create a level
                List<Level> levels = new FilteredElementCollector(_doc).OfClass(typeof(Level)).Cast<Level>().ToList();


                //Obtain the View Template Architectural - FloorPlan
                ViewFamilyType structuralvft = new FilteredElementCollector(_doc).OfClass(typeof(ViewFamilyType))
                    .Cast<ViewFamilyType>()
                    .FirstOrDefault<ViewFamilyType>(x => ViewFamily.FloorPlan == x.ViewFamily);

                int totalOptionsGenerated = 0;
                tbOptionsGenerated.Text = totalOptionsGenerated.ToString();




                ///Start Optimization Process
                while (FloorOverallLength <= maxFloorLength)
                {


                    if (cbTotalIterations.Checked && totalOptionsGenerated >= Convert.ToInt32(tbLimitIterations.Text))
                        break;


                    if (!LevelBuilt)
                    {
                        using (Transaction transBuildLevels = new Transaction(_doc, "Mod: Build Levels"))
                        {
                            transBuildLevels.Start();

                            Dictionary<string, Dictionary<Level, ViewPlan>> Level_VPlan_Collection = new Dictionary<string, Dictionary<Level, ViewPlan>>();


                            //
                            //First FLoor
                            //
                            Level = Level.Create(_doc, TypFloorHeight);

                            if (null == Level)
                                throw new Exception("Create a new level failed.");

                            // Change the level name
                            Level.Name = "Mod Level 1";

                            //Create a New View
                            VPlan = ViewPlan.Create(_doc, structuralvft.Id, Level.Id);
                            VPlan.Name = Level.Name + " - TEST";

                            allLevels.Add(Level);
                            allViewPlans.Add(VPlan);

                            double floorElevIncrement = TypFloorHeight;

                            //Build additional typical foors and Roof floor
                            for (var i = 1; i <= TotalFloors; i++)
                            {
                                //
                                //Next Floor
                                //
                                floorElevIncrement = floorElevIncrement + TypFloorHeight;

                                Level newLevel = Level.Create(_doc, floorElevIncrement);

                                if (null == newLevel)
                                    throw new Exception("Create a new level failed.");

                                // Change the level name
                                if (i < TotalFloors)
                                    newLevel.Name = $"Mod Level {i + 1}";
                                else
                                    newLevel.Name = $"Mod Level Roof";


                                //Create a New View
                                var newViewPlan = ViewPlan.Create(_doc, structuralvft.Id, newLevel.Id);
                                newViewPlan.Name = newLevel.Name + " - TEST";

                                allLevels.Add(newLevel);
                                allViewPlans.Add(newViewPlan);
                            }


                            LevelBuilt = true;

                            transBuildLevels.Commit();
                        }
                    }


                    if (_doc.ActiveView != VPlan)
                    {
                        _uidoc.ActiveView = VPlan;
                        _uidoc.RefreshActiveView();
                    }





                    //
                    //Delete all old elements, if they exist
                    //
                    if (ElementsBuilt.Count > 0)
                    {
                        using (Transaction transDeleteOldMods = new Transaction(_doc, "Mod: Delete Old Mods"))
                        {
                            transDeleteOldMods.Start();

                            foreach (var elemCat in ElementsBuilt)
                            {
                                foreach (var elem in elemCat.Value)
                                {
                                    Element searchElem = _doc.GetElement(elem);

                                    if (searchElem != null)
                                        _doc.Delete(elem);
                                }
                            }

                            ElementsBuilt = new Dictionary<string, List<ElementId>>();

                            transDeleteOldMods.Commit();
                        }
                    }









                    //
                    //
                    //Initialize Floor Layout Class
                    //
                    //
                    FloorLayout floorLayout = new FloorLayout(FloorOverallLength, FloorOverallWidth, FloorHallwayWidth);

                    List<XYPosition> originalFloorEdgePoints = floorLayout.OverallFloorPoints;
                    List<List<XYPosition>> originalFloorHallwayPoints = floorLayout.InternalHallwayPoints;

                    double modIdealLength = floorLayout.IdealModLength;
                    FloorLayout.ModStackType floorScheme = floorLayout.FloorModStackScheme;

                    List<Line> originalFloorEdgeLines = new List<Line>();
                    CurveArray originalFloorEdgeCurveArray = createCurves(originalFloorEdgePoints, TypFloorHeight, out originalFloorEdgeLines);















                    //
                    //Create Base Mod Options
                    //
                    ModBase modBaseStudio = new ModBase("Studio", 1, PodWidthMax, PodWidthMin, PodLengthMax, PodLengthMin, 10);
                    ModBase modBaseOneBed = new ModBase("OneBed", 2, PodWidthMax, PodWidthMin, PodLengthMax, PodLengthMin, 10);
                    ModBase modBaseTwoBed = new ModBase("TwoBed", 3, PodWidthMax, PodWidthMin, PodLengthMax, PodLengthMin, 10);
                    ModBase modBaseCore = new ModBase("Core", 1, PodWidthMax, PodWidthMin, PodLengthMax, PodLengthMin, 10);


                    Dictionary<double, ModOption> optionsStudio = new Dictionary<double, ModOption>();
                    Dictionary<double, ModOption> optionsOneBed = new Dictionary<double, ModOption>();
                    Dictionary<double, ModOption> optionsTwoBed = new Dictionary<double, ModOption>();
                    Dictionary<double, ModOption> optionsCore = new Dictionary<double, ModOption>();

                    //
                    //Create Mod Options of Varying Widths
                    //
                    for (double i = PodWidthMin; i <= PodWidthMax; i++)
                    {
                        ModOption studio = new ModOption(i, modIdealLength, modBaseStudio);
                        ModOption oneBed = new ModOption(i, modIdealLength, modBaseOneBed);
                        ModOption twoBed = new ModOption(i, modIdealLength, modBaseTwoBed);
                        ModOption core = new ModOption(i, modIdealLength, modBaseCore);

                        optionsStudio[i] = studio;
                        optionsOneBed[i] = oneBed;
                        optionsTwoBed[i] = twoBed;
                        optionsCore[i] = core;
                    }







                    //
                    //
                    //Start processing floor layout options.
                    //
                    //
                    double currentModWidth = new Double();
                    currentModWidth = PodWidthMin;

                    while (currentModWidth <= PodWidthMax)
                    {

                        if (cbTotalIterations.Checked && totalOptionsGenerated >= Convert.ToInt32(tbLimitIterations.Text))
                            break;



                        //
                        //Delete all previously created elements, if they exist
                        //
                        if (ElementsBuilt.Count > 0)
                        {
                            using (Transaction transDeleteOldMods = new Transaction(_doc, "Mod: Delete Old Mods"))
                            {
                                transDeleteOldMods.Start();

                                foreach (var elemCat in ElementsBuilt)
                                {
                                    foreach (var elem in elemCat.Value)
                                    {
                                        Element searchElem = _doc.GetElement(elem);

                                        if (searchElem != null)
                                            _doc.Delete(elem);
                                    }
                                }

                                ElementsBuilt = new Dictionary<string, List<ElementId>>();

                                transDeleteOldMods.Commit();
                            }
                        }





                        //
                        //Create unit mix
                        //
                        List<FloorModBlock> floorBlockOptions = new List<FloorModBlock>();
                        int floorBlockCount = 0;

                        for (var i = 1; i <= floorLayout.TotalModBlocks; i++)
                        {

                            double floorBlockWidth = floorLayout.ModBlockWidth[i];
                            double floorBlockLength = floorLayout.ModBlockLength[i];
                            XYPosition currentBlockBasePt = floorLayout.ModBlockBasePt[i];
                            floorLayout.DetermineHallwayPosition();

                            int modsAdded = 0;

                            FloorModBlock currentBlockOption = new FloorModBlock($"{floorBlockCount.ToString()} - {modsAdded.ToString()}", currentBlockBasePt, floorBlockWidth, floorBlockLength, floorLayout);

                            decimal roomAreaFilledRatio = 0;

                            //Overall max room unit fill percentages for entire floor. 
                            decimal unitLayoutRatio_2Bed = (decimal)Percentage2Bed / 100;
                            decimal unitLayoutRatio_1Bed = unitLayoutRatio_2Bed + (decimal)Percentage1Bed / 100;
                            decimal unitLayoutRatio_Studio = unitLayoutRatio_1Bed + (decimal)PercentageStudio / 100;

                            //Overall maxroom unit fill SF values for entire floor. 
                            decimal unitLayoutRatio_2Bed_MaxSF = unitLayoutRatio_2Bed * Convert.ToDecimal(currentBlockOption.SFModTotal);
                            decimal unitLayoutRatio_1Bed_MaxSF = ((decimal)Percentage1Bed / 100) * Convert.ToDecimal(currentBlockOption.SFModTotal) + unitLayoutRatio_2Bed_MaxSF;
                            decimal unitLayoutRatio_Studio_MaxSF = ((decimal)PercentageStudio / 100) * Convert.ToDecimal(currentBlockOption.SFModTotal) + unitLayoutRatio_1Bed_MaxSF;

                            bool addCoreToBlock = false;
                            bool coreModAdded = false;
                            if (i == floorLayout.TotalModBlocks)
                                addCoreToBlock = true;


                            //Fill block with two bed options
                            while (currentBlockOption.ValidateBlockAdd(optionsTwoBed[currentModWidth]) && 
                                (roomAreaFilledRatio <= Convert.ToDecimal(unitLayoutRatio_2Bed)) &&
                                (Convert.ToDecimal(currentBlockOption.SFModFilled) + Convert.ToDecimal(optionsTwoBed[currentModWidth].OverallModArea) <= unitLayoutRatio_2Bed_MaxSF) &&
                                Percentage2Bed != 0)
                            {
                                currentBlockOption.AddModToBlock(optionsTwoBed[currentModWidth]);
                                roomAreaFilledRatio = Decimal.Divide(Convert.ToDecimal(currentBlockOption.SFModFilled), Convert.ToDecimal(currentBlockOption.SFModTotal));

                                if(addCoreToBlock && !coreModAdded && roomAreaFilledRatio > Convert.ToDecimal(0.50))
                                {
                                    currentBlockOption.AddModToBlock(optionsCore[currentModWidth]);
                                    roomAreaFilledRatio = Decimal.Divide(Convert.ToDecimal(currentBlockOption.SFModFilled), Convert.ToDecimal(currentBlockOption.SFModTotal));
                                    coreModAdded = true;
                                }

                                modsAdded++;
                            }

                            //Fill block with one bed options
                            while (currentBlockOption.ValidateBlockAdd(optionsOneBed[currentModWidth]) && 
                                (roomAreaFilledRatio <= Convert.ToDecimal(unitLayoutRatio_1Bed)) &&
                                (Convert.ToDecimal(currentBlockOption.SFModFilled) + Convert.ToDecimal(optionsOneBed[currentModWidth].OverallModArea) <= unitLayoutRatio_1Bed_MaxSF) &&
                                Percentage1Bed != 0)
                            {
                                currentBlockOption.AddModToBlock(optionsOneBed[currentModWidth]);
                                roomAreaFilledRatio = Decimal.Divide(Convert.ToDecimal(currentBlockOption.SFModFilled), Convert.ToDecimal(currentBlockOption.SFModTotal));

                                if (addCoreToBlock && !coreModAdded && roomAreaFilledRatio > Convert.ToDecimal(0.50))
                                {
                                    currentBlockOption.AddModToBlock(optionsCore[currentModWidth]);
                                    roomAreaFilledRatio = Decimal.Divide(Convert.ToDecimal(currentBlockOption.SFModFilled), Convert.ToDecimal(currentBlockOption.SFModTotal));
                                    coreModAdded = true;
                                }

                                modsAdded++;
                            }

                            //Fill remaining block with studio options
                            while (currentBlockOption.ValidateBlockAdd(optionsStudio[currentModWidth]) &&
                                PercentageStudio != 0)
                            {
                                currentBlockOption.AddModToBlock(optionsStudio[currentModWidth]);
                                roomAreaFilledRatio = Decimal.Divide(Convert.ToDecimal(currentBlockOption.SFModFilled), Convert.ToDecimal(currentBlockOption.SFModTotal));

                                if (addCoreToBlock && !coreModAdded && roomAreaFilledRatio > Convert.ToDecimal(0.50))
                                {
                                    currentBlockOption.AddModToBlock(optionsCore[currentModWidth]);
                                    roomAreaFilledRatio = Decimal.Divide(Convert.ToDecimal(currentBlockOption.SFModFilled), Convert.ToDecimal(currentBlockOption.SFModTotal));
                                    coreModAdded = true;
                                }

                                modsAdded++;
                            }


                            floorBlockOptions.Add(currentBlockOption);
                            floorBlockCount++;

                        }



                        //
                        //Initialize FloorLayoutOption Class
                        //

                        FloorLayoutOption floorLayoutOptions = new FloorLayoutOption();
                        foreach (FloorModBlock option in floorBlockOptions)
                        {
                            floorLayoutOptions.AddModBlock(option);
                        }
                        floorLayout.FloorLayoutOptions.Add(floorLayoutOptions);


                        foreach (FloorModBlock option in floorBlockOptions)
                        {
                            option.ModifyModPositions();
                        }



                        //
                        //Draw
                        //

                        List<XYPosition> actualFloorExtentPts = floorLayoutOptions.FloorOverallExtents.getXYPositions();
                        List<Line> actualFloorExtentLines = new List<Line>();
                        CurveArray revisedfloorEdgeCurveArray = createCurves(actualFloorExtentPts, TypFloorHeight, out actualFloorExtentLines);

                        using (Transaction transCreateFloorView = new Transaction(_doc, "Mod: Create Floor"))
                        {
                            transCreateFloorView.Start();

                            // Get a floor type for floor creation
                            FilteredElementCollector collector = new FilteredElementCollector(_doc);
                            collector.OfClass(typeof(FloorType));

                            FloorType floorType = collector.FirstElement() as FloorType;


                            // The normal vector (0,0,1) that must be perpendicular to the profile.
                            XYZ normal = XYZ.BasisZ;

                            Floor newFloor = _doc.Create.NewFloor(revisedfloorEdgeCurveArray, floorType, Level, true, normal);
                            ElementsBuilt["Floor"] = new List<ElementId>() { newFloor.Id };

                            transCreateFloorView.Commit();
                        }




                        List<Line> hallwayLines = new List<Line>();

                        foreach (List<XYPosition> hallway in floorLayoutOptions.InternalHallwayPoints)
                        {
                            List<Line> hallwayLine = new List<Line>();
                            createCurves(hallway, TypFloorHeight, out hallwayLine);
                            hallwayLines.AddRange(hallwayLine);
                        }



                        if (cbDrawOutlineWalls.Checked)
                        {
                            using (Transaction transCreatewalls = new Transaction(_doc, "Mod: Create Walls"))
                            {
                                transCreatewalls.Start();

                                WallType wType = new FilteredElementCollector(_doc).OfClass(typeof(WallType))
                                                    .Cast<WallType>().FirstOrDefault();


                                var wallsBuilt = new List<ElementId>();

                                foreach (var line in hallwayLines)
                                {
                                    var wall = Wall.Create(_doc, line, wType.Id, Level.Id, 10, 0, false, true);
                                    wall.WallType = wType;

                                    wallsBuilt.Add(wall.Id);
                                }

                                foreach (var line in actualFloorExtentLines)
                                {
                                    var wall = Wall.Create(_doc, line, wType.Id, Level.Id, 10, 0, false, true);

                                    wallsBuilt.Add(wall.Id);
                                }

                                ElementsBuilt["Walls - Primary"] = wallsBuilt;


                                transCreatewalls.Commit();
                            }
                        }





                        //
                        //Find all elements in the model
                        //

                        List<Element> AllElem = new FilteredElementCollector(_doc)
                                                .WhereElementIsNotElementType()
                                                .Where(elem => IsPhysicalElement(elem))
                                                .ToList<Element>();

                        List<ElementId> selectedElemsModLab0BDTYPA = new List<ElementId>();
                        List<ElementId> selectedElemsModLab1BDTYPA = new List<ElementId>();
                        List<ElementId> selectedElemsModLab2BDTYPA = new List<ElementId>();
                        List<ElementId> selectedElemsModLabCoreTYPA = new List<ElementId>();
                        List<ElementId> selectedElemsModLabCoreTYPB = new List<ElementId>();

                        Element wallToMoveModLab0BDTYPA_X = null;
                        Element wallToMoveModLab1BDTYPA_X = null;
                        Element wallToMoveModLab2BDTYPA_X = null;
                        Element wallToMoveModLabCoreTYPA_X = null;
                        Element wallToMoveModLabCoreTYPB_X = null;

                        Element wallToMoveModLab0BDTYPA_Y = null;
                        Element wallToMoveModLab1BDTYPA_Y = null;
                        Element wallToMoveModLab2BDTYPA_Y = null;
                        Element wallToMoveModLabCoreTYPA_Y = null;
                        Element wallToMoveModLabCoreTYPB_Y = null;

                        Element centralColModLab0BDTYPA = null;
                        Element centralColModLab1BDTYPA = null;
                        Element centralColModLab2BDTYPA = null;
                        Element centralColModLabCoreTYPA = null;
                        Element centralColModLabCoreTYPB = null;

                        //Find all elements of precreated wall types
                        foreach (var elem in AllElem)
                        {
                            var parComments = elem.LookupParameter("Comments");
                            var parMark = elem.LookupParameter("Mark");

                            if (parComments == null)
                                continue;

                            var commentInfo = parComments.AsString();

                            //ModLab_BD_0_MOD_1_TYPA
                            if (parMark.AsString() == "ModLab_BD_0_MOD_1_TYPA_Center")
                                centralColModLab0BDTYPA = elem;
                            else if (parMark.AsString() == "ModLab_BD_1_MOD_2_TYPA_Center")
                                centralColModLab1BDTYPA = elem;
                            else if (parMark.AsString() == "ModLab_BD_2_MOD_3_TYPA_Center")
                                centralColModLab2BDTYPA = elem;
                            else if (parMark.AsString() == "ModLab_CORE_TYPA_Center")
                                centralColModLabCoreTYPA = elem;
                            else if (parMark.AsString() == "ModLab_CORE_TYPB_Center")
                                centralColModLabCoreTYPB = elem;


                            if (commentInfo == "ModLab_BD_1_MOD_2_TYPA")
                            {
                                selectedElemsModLab1BDTYPA.Add(elem.Id);

                                if (parMark != null)
                                {
                                    var markInfo = parMark.AsString();

                                    if (markInfo == "ModLab_BD_1_MOD_2_TYPA_WallX")
                                    {
                                        wallToMoveModLab1BDTYPA_X = elem;
                                    }
                                    else if (markInfo == "ModLab_BD_1_MOD_2_TYPA_WallY")
                                    {
                                        wallToMoveModLab1BDTYPA_Y = elem;
                                    }

                                }
                            }
                            else if (commentInfo == "ModLab_BD_2_MOD_3_TYPA")
                            {
                                selectedElemsModLab2BDTYPA.Add(elem.Id);

                                if (parMark != null)
                                {
                                    var markInfo = parMark.AsString();

                                    if (markInfo == "ModLab_BD_2_MOD_3_TYPA_WallX")
                                    {
                                        wallToMoveModLab2BDTYPA_X = elem;
                                    }
                                    else if (markInfo == "ModLab_BD_2_MOD_3_TYPA_WallY")
                                    {
                                        wallToMoveModLab2BDTYPA_Y = elem;
                                    }

                                }
                            }
                            else if (commentInfo == "ModLab_BD_0_MOD_1_TYPA")
                            {
                                selectedElemsModLab0BDTYPA.Add(elem.Id);

                                if (parMark != null)
                                {
                                    var markInfo = parMark.AsString();

                                    if (markInfo == "ModLab_BD_0_MOD_1_TYPA_WallX")
                                    {
                                        wallToMoveModLab0BDTYPA_X = elem;
                                    }
                                    else if (markInfo == "ModLab_BD_0_MOD_1_TYPA_WallY")
                                    {
                                        wallToMoveModLab0BDTYPA_Y = elem;
                                    }
                                }
                            }
                            else if (commentInfo == "ModLab_CORE_TYPA")
                            {
                                selectedElemsModLabCoreTYPA.Add(elem.Id);

                                if (parMark != null)
                                {
                                    var markInfo = parMark.AsString();

                                    if (markInfo == "ModLab_CORE_TYPA_WallX")
                                    {
                                        wallToMoveModLabCoreTYPA_X = elem;
                                    }
                                    else if (markInfo == "ModLab_CORE_TYPA_WallY")
                                    {
                                        wallToMoveModLabCoreTYPA_Y = elem;
                                    }
                                }
                            }
                            else if (commentInfo == "ModLab_CORE_TYPB")
                            {
                                selectedElemsModLabCoreTYPB.Add(elem.Id);

                                if (parMark != null)
                                {
                                    var markInfo = parMark.AsString();

                                    if (markInfo == "ModLab_CORE_TYPB_WallX")
                                    {
                                        wallToMoveModLabCoreTYPB_X = elem;
                                    }
                                    else if (markInfo == "ModLab_CORE_TYPB_WallY")
                                    {
                                        wallToMoveModLabCoreTYPB_Y = elem;
                                    }
                                }
                            }


                        }



                        using (Transaction transCreateRegion = new Transaction(_doc, "Mod: Create Flr Elements"))
                        {
                            transCreateRegion.Start();

                            FailureHandlingOptions failOpt = transCreateRegion.GetFailureHandlingOptions();
                            failOpt.SetFailuresPreprocessor(new WarningSwallower());
                            transCreateRegion.SetFailureHandlingOptions(failOpt);


                            FilteredElementCollector fillRegionTypes = new FilteredElementCollector(_doc).OfClass(typeof(FilledRegionType));

                            FilledRegionType solidPattern = (from pattern in fillRegionTypes.Cast<FilledRegionType>()
                                                             where pattern.Name.Equals("Solid Black")
                                                             select pattern).First();

                            var colorStudio = new Autodesk.Revit.DB.Color(245, 194, 66); // Orange
                            var colorOneBed = new Autodesk.Revit.DB.Color(108, 245, 66); // Green
                            var colorTwoBed = new Autodesk.Revit.DB.Color(66, 245, 239); // Blue
                            var colorCore = new Autodesk.Revit.DB.Color(237, 179, 252); // Purple


                            FilledRegionType newPattern0 = findOrCreateSolidFieldRegions("BedStudio", colorStudio);
                            FilledRegionType newPattern1 = findOrCreateSolidFieldRegions("BedOne", colorOneBed);
                            FilledRegionType newPattern2 = findOrCreateSolidFieldRegions("BedTwo", colorTwoBed);
                            FilledRegionType newPattern3 = findOrCreateSolidFieldRegions("Core", colorCore);


                            var regionsBuilt = new List<ElementId>();
                            ElementsBuilt[$"Temp Line"] = new List<ElementId>();
                            ElementsBuilt[$"Room Elements"] = new List<ElementId>();



                            var viewDrafting = new FilteredElementCollector(_doc).OfClass(typeof(ViewDrafting)).OfType<ViewDrafting>().ToList();
                            List<string> viewDraftingNames = (from v in viewDrafting
                                                              select v.Name).ToList();

                            var views = new FilteredElementCollector(_doc).OfClass(typeof(Autodesk.Revit.DB.View)).OfType<Autodesk.Revit.DB.View>().ToList();
                            List<string> viewNames = (from v in views
                                                      select v.Name).ToList();

                            Autodesk.Revit.DB.View viewSource = (from v in views
                                                                 where v.Name == "Level 1"
                                                                 select v).First();


                            for (var j = 0; j < floorBlockOptions.Count; j++)
                            {
                                FloorModBlock blk = floorBlockOptions[j];

                                for (var i = 0; i < blk.PlacedMods.Count; i++)
                                {
                                    List<CurveLoop> profilelps = new List<CurveLoop>();

                                    ModOption currentMod = blk.PlacedMods[i];
                                    XYPosition currentModBasePos = blk.PlacedModsBasePosition[i];

                                    XYPosition4Corners currentModGlobalPoints = blk.PlacedModsOuterPosition[i];

                                    List<Line> lines;

                                    XYPosition pt1 = currentModGlobalPoints.BottomLeft;
                                    XYPosition pt2 = currentModGlobalPoints.TopLeft;
                                    XYPosition pt3 = currentModGlobalPoints.TopRight;
                                    XYPosition pt4 = currentModGlobalPoints.BottomRight;
                                    XYZ ptMid = new XYZ(pt2.X + (pt3.X - pt2.X) / 2, pt1.Y + (pt2.Y - pt1.Y) / 2, TypFloorHeight);

                                    CurveLoop profilelp = createCurveLoop(new List<XYPosition>() { pt1, pt2, pt3, pt4 }, TypFloorHeight, out lines);
                                    profilelps.Add(profilelp);

                                    FilledRegion filledRegion = null;



                                    if (currentMod.TotalMods == 3)
                                    {

                                        filledRegion = FilledRegion.Create(_doc, newPattern2.Id, VPlan.Id, profilelps);


                                        if (cbDrawInteriorLAyout.Checked)
                                        {

                                            bool wallMoved_X = false;
                                            double width = currentMod.UnitModWidth;
                                            double dimToMoveX = (width - 10) * currentMod.TotalMods;

                                            if (wallToMoveModLab2BDTYPA_X != null && dimToMoveX > 0)
                                            {
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab2BDTYPA_X.Id, new XYZ(dimToMoveX, 0, 0));
                                                wallMoved_X = true;
                                            }


                                            bool wallMoved_Y = false;
                                            double length = currentMod.UnitModLength;
                                            double dimToMoveY = length - 30;

                                            if (wallToMoveModLab2BDTYPA_Y != null && dimToMoveY != 0)
                                            {
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab2BDTYPA_Y.Id, new XYZ(0, -dimToMoveY, 0));
                                                wallMoved_Y = true;
                                            }


                                            _uidoc.Selection.SetElementIds(new List<ElementId>() { });
                                            _uidoc.Selection.SetElementIds(selectedElemsModLab2BDTYPA);

                                            LocationPoint locationPt = (LocationPoint)centralColModLab2BDTYPA.Location;
                                            XYZ point = locationPt.Point;
                                            XYZ translationPt = new XYZ(ptMid.X - point.X, ptMid.Y - point.Y, TypFloorHeight);

                                            // Set handler to skip the duplicate types dialog
                                            CopyPasteOptions options = new CopyPasteOptions();
                                            options.SetDuplicateTypeNamesHandler(new HideAndAcceptDuplicateTypeNamesHandler());


                                            var elementsAddedToDelete = ElementTransformUtils.CopyElements(viewSource, selectedElemsModLab2BDTYPA, VPlan, Transform.Identity, options);

                                            if (j == 0)
                                            {
                                                LocationPoint lp = (LocationPoint)centralColModLab2BDTYPA.Location;
                                                XYZ ppt = new XYZ(lp.Point.X, lp.Point.Y, 0);
                                                Line axis = Line.CreateBound(ppt, new XYZ(ppt.X, ppt.Y, ppt.Z + 10));

                                                ElementTransformUtils.RotateElements(_doc, elementsAddedToDelete, axis, Math.PI);
                                            }


                                            var elementsAdded = ElementTransformUtils.CopyElements(_doc, elementsAddedToDelete, translationPt);

                                            foreach (ElementId eId in elementsAdded)
                                            {
                                                Element elem = _doc.GetElement(eId);

                                                var parOffset = elem.LookupParameter("Offset");
                                                if (parOffset != null)
                                                    parOffset.Set(0);
                                            }



                                            _doc.Delete(elementsAddedToDelete);


                                            if (wallMoved_X)
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab2BDTYPA_X.Id, new XYZ(-dimToMoveX, 0, 0));

                                            if (wallMoved_Y)
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab2BDTYPA_Y.Id, new XYZ(0, dimToMoveY, 0));


                                            ElementsBuilt[$"Room Elements"].AddRange(elementsAdded);

                                        }


                                    }
                                    else if (currentMod.TotalMods == 2)
                                    {

                                        filledRegion = FilledRegion.Create(_doc, newPattern1.Id, VPlan.Id, profilelps);


                                        if (cbDrawInteriorLAyout.Checked)
                                        {
                                            bool wallMoved_X = false;
                                            double width = currentMod.UnitModWidth;
                                            double dimToMoveX = (width - 10) * currentMod.TotalMods;

                                            if (wallToMoveModLab1BDTYPA_X != null && dimToMoveX > 0)
                                            {
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab1BDTYPA_X.Id, new XYZ(dimToMoveX, 0, 0));
                                                wallMoved_X = true;
                                            }


                                            bool wallMoved_Y = false;
                                            double length = currentMod.UnitModLength;
                                            double dimToMoveY = length - 30;

                                            if (wallToMoveModLab1BDTYPA_Y != null && dimToMoveY != 0)
                                            {
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab1BDTYPA_Y.Id, new XYZ(0, -dimToMoveY, 0));
                                                wallMoved_Y = true;
                                            }


                                            _uidoc.Selection.SetElementIds(new List<ElementId>() { });
                                            _uidoc.Selection.SetElementIds(selectedElemsModLab1BDTYPA);

                                            LocationPoint locationPt = (LocationPoint)centralColModLab1BDTYPA.Location;
                                            XYZ point = locationPt.Point;
                                            XYZ translationPt = new XYZ(ptMid.X - point.X, ptMid.Y - point.Y, TypFloorHeight);

                                            // Set handler to skip the duplicate types dialog
                                            CopyPasteOptions options = new CopyPasteOptions();
                                            options.SetDuplicateTypeNamesHandler(new HideAndAcceptDuplicateTypeNamesHandler());


                                            var elementsAddedToDelete = ElementTransformUtils.CopyElements(viewSource, selectedElemsModLab1BDTYPA, VPlan, Transform.Identity, options);

                                            if (j == 0)
                                            {
                                                LocationPoint lp = (LocationPoint)centralColModLab1BDTYPA.Location;
                                                XYZ ppt = new XYZ(lp.Point.X, lp.Point.Y, 0);
                                                Line axis = Line.CreateBound(ppt, new XYZ(ppt.X, ppt.Y, ppt.Z + 10));

                                                ElementTransformUtils.RotateElements(_doc, elementsAddedToDelete, axis, Math.PI);
                                            }

                                            var elementsAdded = ElementTransformUtils.CopyElements(_doc, elementsAddedToDelete, translationPt);

                                            foreach (ElementId eId in elementsAdded)
                                            {
                                                Element elem = _doc.GetElement(eId);

                                                var parOffset = elem.LookupParameter("Offset");
                                                if (parOffset != null)
                                                    parOffset.Set(0);
                                            }


                                            _doc.Delete(elementsAddedToDelete);

                                            if (wallMoved_X)
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab1BDTYPA_X.Id, new XYZ(-dimToMoveX, 0, 0));

                                            if (wallMoved_Y)
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab1BDTYPA_Y.Id, new XYZ(0, dimToMoveY, 0));

                                            ElementsBuilt[$"Room Elements"].AddRange(elementsAdded);
                                        }




                                    }
                                    else if (currentMod.TotalMods == 1 && currentMod.ModOptionName.Contains("Core"))
                                    {

                                        filledRegion = FilledRegion.Create(_doc, newPattern3.Id, VPlan.Id, profilelps);


                                        if (cbDrawInteriorLAyout.Checked)
                                        {
                                            bool wallMoved_X = false;
                                            double width = currentMod.UnitModWidth;
                                            double intialWidth = 10;

                                            double dimToMoveX = (width - intialWidth) * currentMod.TotalMods;

                                            if (wallToMoveModLabCoreTYPA_X != null && dimToMoveX > 0)
                                            {
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLabCoreTYPA_X.Id, new XYZ(dimToMoveX, 0, 0));
                                                wallMoved_X = true;
                                            }



                                            bool wallMoved_Y = false;
                                            double length = currentMod.UnitModLength;
                                            double dimToMoveY = length - 30;


                                            if (wallToMoveModLabCoreTYPA_Y != null && dimToMoveY != 0)
                                            {
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLabCoreTYPA_Y.Id, new XYZ(0, -dimToMoveY, 0));
                                                wallMoved_Y = true;
                                            }



                                            _uidoc.Selection.SetElementIds(new List<ElementId>() { });
                                            _uidoc.Selection.SetElementIds(selectedElemsModLabCoreTYPA);

                                            LocationPoint locationPt = (LocationPoint)centralColModLabCoreTYPA.Location;
                                            XYZ point = locationPt.Point;
                                            XYZ translationPt = new XYZ(ptMid.X - point.X, ptMid.Y - point.Y, TypFloorHeight);

                                            // Set handler to skip the duplicate types dialog
                                            CopyPasteOptions options = new CopyPasteOptions();
                                            options.SetDuplicateTypeNamesHandler(new HideAndAcceptDuplicateTypeNamesHandler());


                                            var elementsAddedToDelete = ElementTransformUtils.CopyElements(viewSource, selectedElemsModLabCoreTYPA, VPlan, Transform.Identity, options);

                                            if (j == 0)
                                            {
                                                LocationPoint lp = (LocationPoint)centralColModLabCoreTYPA.Location;
                                                XYZ ppt = new XYZ(lp.Point.X, lp.Point.Y, 0);
                                                Line axis = Line.CreateBound(ppt, new XYZ(ppt.X, ppt.Y, ppt.Z + 10));

                                                ElementTransformUtils.RotateElements(_doc, elementsAddedToDelete, axis, Math.PI);
                                            }

                                            var elementsAdded = ElementTransformUtils.CopyElements(_doc, elementsAddedToDelete, translationPt);


                                            foreach (ElementId eId in elementsAdded)
                                            {
                                                Element elem = _doc.GetElement(eId);

                                                var parOffset = elem.LookupParameter("Offset");
                                                if (parOffset != null)
                                                    parOffset.Set(0);

                                            }


                                            _doc.Delete(elementsAddedToDelete);

                                            if (wallMoved_X)
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLabCoreTYPA_X.Id, new XYZ(-dimToMoveX, 0, 0));

                                            if (wallMoved_Y)
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLabCoreTYPA_Y.Id, new XYZ(0, dimToMoveY, 0));


                                            ElementsBuilt[$"Room Elements"].AddRange(elementsAdded);

                                        }


                                    }
                                    else
                                    {
                                        filledRegion = FilledRegion.Create(_doc, newPattern0.Id, VPlan.Id, profilelps);

                                        if (cbDrawInteriorLAyout.Checked)
                                        {
                                            bool wallMoved_X = false;
                                            double width = currentMod.UnitModWidth;
                                            double intialWidth = 10;

                                            double dimToMoveX = (width - intialWidth) * currentMod.TotalMods;

                                            if (wallToMoveModLab0BDTYPA_X != null && dimToMoveX > 0)
                                            {
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab0BDTYPA_X.Id, new XYZ(dimToMoveX, 0, 0));
                                                wallMoved_X = true;
                                            }



                                            bool wallMoved_Y = false;
                                            double length = currentMod.UnitModLength;
                                            double dimToMoveY = length - 30;


                                            if (wallToMoveModLab0BDTYPA_Y != null && dimToMoveY != 0)
                                            {
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab0BDTYPA_Y.Id, new XYZ(0, -dimToMoveY, 0));
                                                wallMoved_Y = true;
                                            }


                                            _uidoc.Selection.SetElementIds(new List<ElementId>() { });
                                            _uidoc.Selection.SetElementIds(selectedElemsModLab0BDTYPA);

                                            LocationPoint locationPt = (LocationPoint)centralColModLab0BDTYPA.Location;
                                            XYZ point = locationPt.Point;
                                            XYZ translationPt = new XYZ(ptMid.X - point.X, ptMid.Y - point.Y, TypFloorHeight);

                                            // Set handler to skip the duplicate types dialog
                                            CopyPasteOptions options = new CopyPasteOptions();
                                            options.SetDuplicateTypeNamesHandler(new HideAndAcceptDuplicateTypeNamesHandler());


                                            var elementsAddedToDelete = ElementTransformUtils.CopyElements(viewSource, selectedElemsModLab0BDTYPA, VPlan, Transform.Identity, options);

                                            if (j == 0)
                                            {
                                                LocationPoint lp = (LocationPoint)centralColModLab0BDTYPA.Location;
                                                XYZ ppt = new XYZ(lp.Point.X, lp.Point.Y, 0);
                                                Line axis = Line.CreateBound(ppt, new XYZ(ppt.X, ppt.Y, ppt.Z + 10));

                                                ElementTransformUtils.RotateElements(_doc, elementsAddedToDelete, axis, Math.PI);
                                            }

                                            var elementsAdded = ElementTransformUtils.CopyElements(_doc, elementsAddedToDelete, translationPt);


                                            foreach (ElementId eId in elementsAdded)
                                            {
                                                Element elem = _doc.GetElement(eId);

                                                var parOffset = elem.LookupParameter("Offset");
                                                if (parOffset != null)
                                                    parOffset.Set(0);

                                            }


                                            _doc.Delete(elementsAddedToDelete);

                                            if (wallMoved_X)
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab0BDTYPA_X.Id, new XYZ(-dimToMoveX, 0, 0));

                                            if (wallMoved_Y)
                                                ElementTransformUtils.MoveElement(_doc, wallToMoveModLab0BDTYPA_Y.Id, new XYZ(0, dimToMoveY, 0));


                                            ElementsBuilt[$"Room Elements"].AddRange(elementsAdded);



                                        }

                                    }



                                    ////Draw temp line at mod top left corner
                                    //Line lineA = Line.CreateBound(new XYZ(pt2.X, pt2.Y, elevation), new XYZ(pt2.X + 10, pt2.Y + 10, elevation));
                                    //ModelLine line = _doc.Create.NewModelCurve(lineA, vplan.SketchPlane) as ModelLine;
                                    //elementsBuilt[$"Temp Line"].Add(line.Id);

                                    if (filledRegion != null)
                                    regionsBuilt.Add(filledRegion.Id);
                                }
                            }

                            ElementsBuilt["Mod Regions"] = regionsBuilt;






                            //
                            //Replicate first floor to all other typical floors
                            //
                            bool addMultiplefloors = true;

                            //Create copy of intially created first floor elements - to be used to copy to other floors. 
                            Dictionary<string, List<ElementId>> elementsBuiltFirstFloor = new Dictionary<string, List<ElementId>>(ElementsBuilt);

                            //Copy floor elements to all typical floors - Except Roof Floor
                            for (var i = 1; i < allLevels.Count - 1; i++)
                            {
                                Level currentLevel = allLevels[i];
                                ViewPlan currentViewPlan = allViewPlans[i];

                                if (!addMultiplefloors)
                                    continue;

                                Dictionary<string, List<ElementId>> copiedElements = new Dictionary<string, List<ElementId>>();

                                foreach (var elementToCopy in elementsBuiltFirstFloor)
                                {
                                    string elemType = elementToCopy.Key;
                                    List<ElementId> elemList = elementToCopy.Value;

                                    if (elemList.Count == 0)
                                        continue;

                                    ICollection<ElementId> elemIds = ElementTransformUtils.CopyElements(VPlan, elemList, currentViewPlan, null, null);

                                    copiedElements[elemType + $"_{i + 1}"] = elemIds.ToList();

                                }

                                foreach (var k in copiedElements)
                                {
                                    ElementsBuilt.Add(k.Key, k.Value);
                                }


                            }




                            //
                            //Ensure all walls and floors are not offset
                            //
                            foreach (var elemList in ElementsBuilt.Values)
                            {
                                foreach (var elemId in elemList)
                                {
                                    var element = _doc.GetElement(elemId);
                                    if (element.Category.Name == "Walls")
                                    {
                                        var par = element.LookupParameter("Top Offset");

                                        if (par != null)
                                        {
                                            var offsetDim = par.AsDouble();
                                            if (offsetDim != 0)
                                                par.Set(0);
                                        }

                                    }
                                    else if (element.Category.Name == "Floors")
                                    {
                                        var par = element.LookupParameter("Height Offset From Level");

                                        if (par != null)
                                        {
                                            var offsetDim = par.AsDouble();
                                            if (offsetDim != 0)
                                                par.Set(0);
                                        }

                                    }

                                }
                            }


                            _doc.Regenerate();
                            transCreateRegion.Commit();
                        }

                        List<UIView> openViews = _uidoc.GetOpenUIViews().ToList();
                        foreach (var v in openViews)
                        {
                            if (v.ViewId == VPlan.Id)
                            {
                                //v.ZoomToFit();
                                _uidoc.ActiveView = VPlan;
                                v.ZoomAndCenterRectangle(new XYZ(-25, -25, 0), new XYZ(150, 150, 0));
                            }
                        }

                        currentModWidth = currentModWidth + 1;

                        totalOptionsGenerated++;
                        tbOptionsGenerated.Text = totalOptionsGenerated.ToString();
                        _uidoc.RefreshActiveView();

                    }


                    FloorLayoutOptions.Add(floorLayout);

                    FloorOverallLength = FloorOverallLength + 20;
                    _uidoc.RefreshActiveView();

                }



            }
            catch (Exception ex)
            {
                var message = ex.Message;
                TaskDialog.Show("Error", message);
            }

        }




        class HideAndAcceptDuplicateTypeNamesHandler : IDuplicateTypeNamesHandler
        {
            #region IDuplicateTypeNamesHandler Members

            /// <summary>
            /// Implementation of the IDuplicateTypeNameHandler
            /// </summary>
            /// <param name="args"></param>
            /// <returns></returns>
            public DuplicateTypeAction OnDuplicateTypeNamesFound(DuplicateTypeNamesHandlerArgs args)
            {
                // Always use duplicate destination types when asked
                return DuplicateTypeAction.UseDestinationTypes;
            }

            #endregion
        }



        private CurveLoop createCurveLoop(List<XYPosition> xyPoints, double elevation, out List<Line> lines)
        {
            CurveLoop curveLoop = new CurveLoop();
            lines = new List<Line>();

            for (var i = 0; i < xyPoints.Count; i++)
            {
                XYPosition p1;
                XYPosition p2;
                XYZ pt1;
                XYZ pt2;
                Line line;

                //If only two points are present, then create single line
                if (xyPoints.Count == 2)
                {
                    p1 = xyPoints[i];
                    p2 = xyPoints[i + 1];

                    pt1 = new XYZ(p1.X, p1.Y, elevation);
                    pt2 = new XYZ(p2.X, p2.Y, elevation);
                    line = Line.CreateBound(pt1, pt2);

                    lines.Add(line);
                    curveLoop.Append(line);

                    break;
                }

                //If more than two points exist then create a loop
                if (i == xyPoints.Count - 1)
                {
                    p1 = xyPoints[i];
                    p2 = xyPoints[0];
                }
                else
                {
                    p1 = xyPoints[i];
                    p2 = xyPoints[i + 1];
                }

                pt1 = new XYZ(p1.X, p1.Y, elevation);
                pt2 = new XYZ(p2.X, p2.Y, elevation);
                line = Line.CreateBound(pt1, pt2);

                lines.Add(line);
                curveLoop.Append(line);
            }


            return curveLoop;

        }





        private CurveArray createCurves(List<XYPosition> xyPoints, double elevation, out List<Line> lines)
        {
            CurveArray overallFloorProfile = new CurveArray();
            lines = new List<Line>();

            for (var i = 0; i < xyPoints.Count; i++)
            {
                XYPosition p1;
                XYPosition p2;
                XYZ pt1;
                XYZ pt2;
                Line line;

                //If only two points are present, then create single line
                if (xyPoints.Count == 2)
                {
                    p1 = xyPoints[i];
                    p2 = xyPoints[i + 1];

                    pt1 = new XYZ(p1.X, p1.Y, elevation);
                    pt2 = new XYZ(p2.X, p2.Y, elevation);
                    line = Line.CreateBound(pt1, pt2);

                    lines.Add(line);
                    overallFloorProfile.Append(line);

                    break;
                }

                //If more than two points exist then create a loop
                if (i == xyPoints.Count - 1)
                {
                    p1 = xyPoints[i];
                    p2 = xyPoints[0];
                }
                else
                {
                    p1 = xyPoints[i];
                    p2 = xyPoints[i + 1];
                }

                pt1 = new XYZ(p1.X, p1.Y, elevation);
                pt2 = new XYZ(p2.X, p2.Y, elevation);
                line = Line.CreateBound(pt1, pt2);

                lines.Add(line);
                overallFloorProfile.Append(line);
            }

            return overallFloorProfile;
        }




        private FilledRegionType findOrCreateSolidFieldRegions(string filledRegionsToCreate, Autodesk.Revit.DB.Color color)
        {
            var newPattern = new FilteredElementCollector(_doc)
                    .OfClass(typeof(FilledRegionType))
                    .Cast<FilledRegionType>()
                    .FirstOrDefault(q => q.Name == filledRegionsToCreate) as FilledRegionType;

            var solidPattern = new FilteredElementCollector(_doc)
                    .OfClass(typeof(FilledRegionType))
                    .Cast<FilledRegionType>()
                    .FirstOrDefault(q => q.Name == "Solid Black") as FilledRegionType;


            if (newPattern == null)
            {
                newPattern = solidPattern.Duplicate(filledRegionsToCreate) as FilledRegionType;

                newPattern.BackgroundPatternColor = color;

                newPattern.ForegroundPatternColor = color;
            }

            return newPattern;

        }


        private bool IsPhysicalElement(Element e)
        {

            if (e.Category == null) return false;

            if (e.ViewSpecific) return false;

            // exclude specific unwanted categories
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_HVAC_Zones) return false;

            return e.Category.CategoryType == CategoryType.Model && e.Category.CanAddSubcategory;
        }




        /// <summary>
        /// Close active form. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            formClosed = true;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadDataFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt" })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (openFileDialog.FileName != null)
                        {
                            string content = File.ReadAllText(openFileDialog.FileName);

                            //Parse JSON and return value
                            JObject jsonContentObj = Newtonsoft.Json.Linq.JObject.Parse(content);
                            var deserializeObject = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                            JObject json = JObject.Parse(content);

                            DictJSON = new Dictionary<string, string>();
                            List<string> keys = new List<string>();
                            List<string> values = new List<string>();

                            foreach (var x in jsonContentObj)
                            {
                                string name = x.Key;
                                JToken value = x.Value;

                                while (value.HasValues)
                                {
                                    Type t = value.GetType();


                                    //
                                    //If item is an Array
                                    //
                                    if (t.Name == "JArray")
                                    {
                                        var arr = value.ToArray();

                                        foreach (JToken x1 in arr)
                                        {

                                            if (x1.GetType().Name == "JObject")
                                            {
                                                var obj = x1.ToObject<JObject>();

                                                foreach (var x2 in obj)
                                                {
                                                    string name1 = x2.Key;
                                                    JToken value1 = x2.Value;

                                                    if (value1.HasValues)
                                                    {
                                                        value = value1;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        keys.Add(name1);
                                                        values.Add(value1.ToString());
                                                        DictJSON[name1] = value1.ToString();
                                                    }
                                                }

                                            }
                                        }

                                        break;

                                    }
                                    //
                                    //If item is an object
                                    //
                                    else if (t.Name == "JObject")
                                    {

                                        var obj = value.ToObject<JObject>();

                                        foreach (var x1 in obj)
                                        {
                                            string name1 = x1.Key;
                                            JToken value1 = x1.Value;

                                            if (value1.HasValues)
                                            {
                                                value = value1;
                                                break;
                                            }
                                            else
                                            {
                                                keys.Add(name1);
                                                values.Add(value1.ToString());
                                                DictJSON[name1] = value1.ToString();
                                            }
                                        }

                                        break;

                                    }

                                }

                                if (!value.HasValues)
                                {
                                    keys.Add(name);
                                    values.Add(value.ToString());
                                    DictJSON[name] = value.ToString();
                                }


                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("ERROR", ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void btnViewJSON_Click(object sender, EventArgs e)
        {
            if (DictJSON.Count == 0)
            {
                TaskDialog.Show("JSON", "JSON data empty.");
                return;
            }

            try
            {

                //Pop-out new form to display beam list
                using (Stacker.Commands.FormViewJsonData frmJsonViewer = new FormViewJsonData(DictJSON))
                {
                    frmJsonViewer.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                TaskDialog.Show("ERROR", ex.Message);
            }


        }

        /// <summary>
        /// Suppress warnings and errors from user. 
        /// </summary>
        public class WarningSwallower : IFailuresPreprocessor
        {
            public FailureProcessingResult PreprocessFailures(FailuresAccessor a)
            {
                IList<FailureMessageAccessor> failures = a.GetFailureMessages();

                foreach (FailureMessageAccessor f in failures)
                {
                    FailureDefinitionId id = f.GetFailureDefinitionId();

                    FailureSeverity failureSeverity = a.GetSeverity();

                    if (failureSeverity == FailureSeverity.Warning)
                    {
                        a.DeleteWarning(f);
                    }
                    else if (failureSeverity == FailureSeverity.Error)
                    {

                        List<ElementId> failingElementIds = f.GetFailingElementIds().ToList();
                        FailureHandlingOptions failureHandOptions = a.GetFailureHandlingOptions();
                        List<FailureResolutionType> resolutionTypes = a.GetAttemptedResolutionTypes(f).ToList();

                        a.ResolveFailure(f);

                        return FailureProcessingResult.ProceedWithCommit;

                    }
                    else
                    {
                        return FailureProcessingResult.ProceedWithRollBack;
                    }
                }
                return FailureProcessingResult.Continue;
            }
        }







        /// <summary>
        /// Round down to nearest integer to calculate total floors. 
        /// </summary>
        /// <param name="floorHeight"></param>
        /// <param name="totalBldgHeight"></param>
        /// <returns></returns>
        private int calculateTotalFloors(double floorHeight, double totalBldgHeight)
        {
            int totalFloors = Convert.ToInt32(Math.Floor(totalBldgHeight / floorHeight));

            return totalFloors;
        }


        /// <summary>
        /// Input values validation. 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>Bool: true text value is valid.</returns>
        private bool validateInputTextIsNumeric(string value, out string errorMessage)
        {
            bool textValid = true;
            errorMessage = "";

            try
            {

                if (value == "")
                {
                    errorMessage += "Please enter numeric value. \r\n";
                    textValid = false;
                }

                if (value == "0")
                {
                    errorMessage += "Value has to be greater than 0. \r\n";
                    textValid = false;

                }

                double distance = Convert.ToDouble(value);

                if (distance < 0)
                {
                    errorMessage += "Minimum square footage of openings must be greater than 0. \r\n";
                    textValid = false;

                }


            }
            catch (Exception ex)
            {
                errorMessage += ex.Message;
            }

            return textValid;
        }





        /// <summary>
        /// Calculate total stories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplyFloorChanges_Click(object sender, EventArgs e)
        {
            string error1 = "";
            string error2 = "";

            bool textValid1 = validateInputTextIsNumeric(tbTypStoryHeight.Text, out error1);
            bool textValid2 = validateInputTextIsNumeric(tbTotalBuildingHeight.Text, out error2);

            if (textValid1 && textValid2)
            {
                double flrHeight = Convert.ToDouble(tbTypStoryHeight.Text);
                double bldgHeight = Convert.ToDouble(tbTotalBuildingHeight.Text);

                int totalFloors = calculateTotalFloors(flrHeight, bldgHeight);

                tbMaxFloors.Text = Convert.ToString(totalFloors);

                if (!cbTotalFloors.Checked)
                    tbFloorsTotal.Text = tbMaxFloors.Text;
            }


        }


        /// <summary>
        /// Logic to overwrite total floors in the building. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTotalFloors_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTotalFloors.Checked)
            {
                tbFloorsTotal.Enabled = true;
            }
            else
            {
                tbFloorsTotal.Enabled = false;
                tbFloorsTotal.Text = tbMaxFloors.Text;
            }
        }




        /// <summary>
        /// Verify that the pressed key isn't CTRL or any non-numeric digit
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool validateKeyPressIsNumeric(KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                return true;
            }

            return false;
        }



        private void tbPercentageStudio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (validateKeyPressIsNumeric(e))
                e.Handled = true;
        }

        private void tbPercentage1Bed_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (validateKeyPressIsNumeric(e))
                e.Handled = true;
        }

        private void tbPercentage2Bed_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (validateKeyPressIsNumeric(e))
                e.Handled = true;
        }


        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTypStoryHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (validateKeyPressIsNumeric(e))
                e.Handled = true;
        }

        /// <summary>
        /// Avoid entering non numeric info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTotalBuildingHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (validateKeyPressIsNumeric(e))
                e.Handled = true;
        }


        /// <summary>
        /// Avoid entering non numeric info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTotalFloors_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (validateKeyPressIsNumeric(e))
                e.Handled = true;
        }




    }
}
