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
using Stacker.GeoJsonClasses;
using System.IO;
using Stacker.ModClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using View = Autodesk.Revit.DB.View;
using EPPlus = OfficeOpenXml;
using RestSharp;
using RestSharp.Serialization.Json;
using Parameter = Autodesk.Revit.DB.Parameter;
using CoordinateSharp;
using Stacker.Commands;

namespace Stacker.GeoJsonClasses
{
    public partial class CreatePrelimLayoutForm : System.Windows.Forms.Form
    {

        #region Properties

        private Document _doc;
        private UIDocument _uidoc;

        public double FloorOverallLength { get; set; }
        public double FloorOverallWidth { get; set; }
        public double FloorHallwayWidth { get; set; }
        public double FloorOverallSquareFootage { get; set; }

        public double PodLengthMin { get; set; }
        public double PodLengthMax { get; set; }
        public double PodWidthMin { get; set; }
        public double PodWidthMax { get; set; }

        public int PriorityStudio { get; set; }
        public int Priority1Bed { get; set; }
        public int Priority2Bed { get; set; }

        public double PercentageStudio { get; set; }
        public double Percentage1Bed { get; set; }
        public double Percentage2Bed { get; set; }

        public Dictionary<string, List<ElementId>> ElementsBuilt { get; set; }
        List<FloorLayout> FloorLayoutOptions { get; set; }
        Dictionary<string, string> DictJSON { get; set; }
        public Level Level { get; set; }
        public ViewPlan VPlan { get; set; }
        public List<Level> AllLevels { get; set; }
        public List<ViewPlan> AllViewPlans { get; set; }


        double TypFloorHeight = 12.0;
        int TotalFloors = 1;

        public bool formClosed = false;
        public bool LevelBuilt = false;


        #endregion




        #region Form Constructor

        public CreatePrelimLayoutForm(Document doc, UIDocument uidoc)
        {
            InitializeComponent();

            _doc = doc;
            _uidoc = uidoc;

            PodLengthMin = 20;
            PodLengthMax = 35;
            PodWidthMin = 12;
            PodWidthMax = 16;

            tbModLengthMin.Text = PodLengthMin.ToString();
            tbModLengthMax.Text = PodLengthMax.ToString();
            tbModWidthMin.Text = PodWidthMin.ToString();
            tbModWidthMax.Text = PodWidthMax.ToString();

            FloorLayoutOptions = new List<FloorLayout>();
            DictJSON = new Dictionary<string, string>();
            ElementsBuilt = new Dictionary<string, List<ElementId>>();
            AllLevels = new List<Level>();
            AllViewPlans = new List<ViewPlan>();

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

        #endregion




        #region Form Button Logic
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



        /// <summary>
        /// Delete All Existing Built Geometry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Close active form. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            formClosed = true;
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





        #endregion





        #region Main Build Layout Method

        /// <summary>
        /// Entire process of building floor layouts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuildLayout_Click(object sender, EventArgs e)
        {
            try
            {
                FloorLayoutOptions = new List<FloorLayout>();

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

                            AllLevels.Add(Level);
                            AllViewPlans.Add(VPlan);

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

                                AllLevels.Add(newLevel);
                                AllViewPlans.Add(newViewPlan);
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

                        Autodesk.Revit.DB.XYZ hallwayMidPt = hallwayLines[2].Evaluate(0.5, true);


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

                        List<Element> allElemInModel = new FilteredElementCollector(_doc)
                                                .WhereElementIsNotElementType()
                                                .Where(elem => IsPhysicalElement(elem))
                                                .ToList<Element>();

                        List<ElementId> selectedElemsModLab0BDTYPA = new List<ElementId>();
                        List<ElementId> selectedElemsModLab1BDTYPA = new List<ElementId>();
                        List<ElementId> selectedElemsModLab2BDTYPA = new List<ElementId>();
                        List<ElementId> selectedElemsModLabCoreTYPA = new List<ElementId>();
                        List<ElementId> selectedElemsModLabCoreTYPB = new List<ElementId>();
                        List<ElementId> selectedElementsHallway = new List<ElementId>();

                        Element wallToMoveModLab0BDTYPA_X = null;
                        Element wallToMoveModLab1BDTYPA_X = null;
                        Element wallToMoveModLab2BDTYPA_X = null;
                        Element wallToMoveModLabCoreTYPA_X = null;
                        Element wallToMoveModLabCoreTYPB_X = null;
                        Element wallToMoveHallway_X = null;

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
                        Element centralColHallway = null;

                        //Find all elements of precreated wall types
                        foreach (var elem in allElemInModel)
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
                            else if (parMark.AsString() == "ModLab_Hallway_Center")
                                centralColHallway = elem;

                            if (commentInfo == "ModLab_Hallway")
                            {
                                selectedElementsHallway.Add(elem.Id);

                                if (parMark != null)
                                {
                                    var markInfo = parMark.AsString();

                                    if (markInfo == "ModLab_Hallway_WallX")
                                    {
                                        wallToMoveHallway_X = elem;
                                    }
                                }
                            }
                            else if (commentInfo == "ModLab_BD_1_MOD_2_TYPA")
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
                                        var parComments = filledRegion.LookupParameter("Comments");
                                        parComments.Set(_doc.GetElement(selectedElemsModLab2BDTYPA[0]).LookupParameter("Comments").AsString());

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
                                        var parComments = filledRegion.LookupParameter("Comments");
                                        parComments.Set(_doc.GetElement(selectedElemsModLab1BDTYPA[0]).LookupParameter("Comments").AsString());

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
                                        var parComments = filledRegion.LookupParameter("Comments");
                                        parComments.Set(_doc.GetElement(selectedElemsModLabCoreTYPA[0]).LookupParameter("Comments").AsString());

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
                                        var parComments = filledRegion.LookupParameter("Comments");
                                        parComments.Set(_doc.GetElement(selectedElemsModLab0BDTYPA[0]).LookupParameter("Comments").AsString());

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
                            //Create hallway walls and floor
                            //
                            if (cbDrawInteriorLAyout.Checked)
                            {
                                bool wallMoved_X = false;
                                double overallLength = floorBlockOptions[0].BlockLengthUsed;
                                double dimToMoveX = (overallLength - 10);

                                if (wallToMoveHallway_X != null && dimToMoveX > 0)
                                {
                                    ElementTransformUtils.MoveElement(_doc, wallToMoveHallway_X.Id, new XYZ(dimToMoveX, 0, 0));
                                    wallMoved_X = true;
                                }


                                _uidoc.Selection.SetElementIds(new List<ElementId>() { });
                                _uidoc.Selection.SetElementIds(selectedElementsHallway);

                                LocationPoint locationPt = (LocationPoint)centralColHallway.Location;
                                XYZ point = locationPt.Point;
                                XYZ translationPt = new XYZ(hallwayMidPt.X - point.X, hallwayMidPt.Y - point.Y, TypFloorHeight);

                                // Set handler to skip the duplicate types dialog
                                CopyPasteOptions options = new CopyPasteOptions();
                                options.SetDuplicateTypeNamesHandler(new HideAndAcceptDuplicateTypeNamesHandler());


                                var elementsAddedToDelete = ElementTransformUtils.CopyElements(viewSource, selectedElementsHallway, VPlan, Transform.Identity, options);


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
                                    ElementTransformUtils.MoveElement(_doc, wallToMoveHallway_X.Id, new XYZ(-dimToMoveX, 0, 0));


                                ElementsBuilt[$"Hallway Elements"] = elementsAdded.ToList();

                            }







                            //
                            //Find an Delete Overlapping walls
                            //

                            //Create list of walls where bounding boxes are matching
                            List<Wall> wallsToRemove = new List<Wall>();

                            foreach (var elem in ElementsBuilt)
                            {
                                string elemCategoryName = elem.Key;
                                List<ElementId> elemIDs = elem.Value;

                                if (!elemCategoryName.Contains("Room Elements"))
                                {
                                    continue;
                                }

                                for (int i = 0; i <= elemIDs.Count() - 2; i++)
                                {
                                    ElementId elemID = elemIDs[i];

                                    Wall wall = _doc.GetElement(elemID) as Wall;

                                    if (wall == null)
                                        continue;

                                    BoundingBoxXYZ bb = wall.get_BoundingBox(null);
                                    var max = bb.Max;
                                    var min = bb.Min;

                                    for (int j = i + 1; j <= elemIDs.Count() - 1; j++)
                                    {
                                        ElementId elemID2 = elemIDs[j];
                                        Wall wall2 = _doc.GetElement(elemID2) as Wall;

                                        if (wall2 == null)
                                            continue;

                                        BoundingBoxXYZ bb2 = wall2.get_BoundingBox(null);
                                        var max2 = bb2.Max;
                                        var min2 = bb2.Min;
                                        
                                        //TODO dangerous to have 0 digits tolerance, but right now its working to find overlapping walls within a close range
                                        if (arePointsSimilar(max, max2, 0) && arePointsSimilar(min, min2, 0))
                                        {
                                            wallsToRemove.Add(wall2);
                                            break;
                                        }


                                    }
                                }
                            }


                            //Remove walls from main ElementsBuilt object
                            foreach (var wall in wallsToRemove)
                            {
                                foreach (var elem in ElementsBuilt)
                                {
                                    string elemCategoryName = elem.Key;
                                    List<ElementId> elemIDs = elem.Value;

                                    if (!elemCategoryName.Contains("Room Elements"))
                                        continue;

                                    for (int i = 0; i < elemIDs.Count(); i++)
                                    {
                                        ElementId elemID = elemIDs[i];
                                        Wall wallToCheck = _doc.GetElement(elemID) as Wall;

                                        if (wallToCheck == null)
                                            continue;

                                        if (wall.Id == wallToCheck.Id)
                                        {
                                            elemIDs.RemoveAt(i);
                                            _doc.Delete(wall.Id);
                                            break;
                                        }

                                    }
                                }

                                
                            }


                            transCreateRegion.Commit();
                        }



                        //
                        //Validate all elements are still existing in model. TODO if elements are null try to fix the issue so they are not null
                        //
                        foreach(var cat in ElementsBuilt)
                        {
                            cat.Value.RemoveAll(id => _doc.GetElement(id) == null);
                        }





                        //
                        //Create floors and copy base level up
                        //
                        using (Transaction transCreateFloors = new Transaction(_doc, "Mod: Create Floors"))
                        {
                            transCreateFloors.Start();

                            FailureHandlingOptions failOpt = transCreateFloors.GetFailureHandlingOptions();
                            failOpt.SetFailuresPreprocessor(new WarningSwallower());
                            transCreateFloors.SetFailureHandlingOptions(failOpt);

                            //
                            //Replicate first floor to all other typical floors
                            //
                            bool addMultiplefloors = true;

                            //Create copy of intially created first floor elements - to be used to copy to other floors. 
                            Dictionary<string, List<ElementId>> elementsBuiltFirstFloor = new Dictionary<string, List<ElementId>>(ElementsBuilt);

                            //Copy floor elements to all typical floors - Except Roof Floor
                            for (var i = 1; i < AllLevels.Count; i++)
                            {
                                Level currentLevel = AllLevels[i];
                                ViewPlan currentViewPlan = AllViewPlans[i];

                                if (!addMultiplefloors)
                                    continue;

                                Dictionary<string, List<ElementId>> copiedElements = new Dictionary<string, List<ElementId>>();
                                
                                if(i == AllLevels.Count - 1)
                                {
                                    var floorElements = elementsBuiltFirstFloor["Floor"];

                                    foreach (var elementToCopy in floorElements)
                                    {
                                        string elemType = "Floor";

                                        ICollection<ElementId> elemIds = ElementTransformUtils.CopyElements(VPlan, new List<ElementId>() { elementToCopy }, currentViewPlan, null, null);

                                        copiedElements[elemType + $"_Roof"] = elemIds.ToList();

                                    }

                                }
                                else
                                {
                                    foreach (var elementToCopy in elementsBuiltFirstFloor)
                                    {
                                        string elemType = elementToCopy.Key;
                                        List<ElementId> elemList = elementToCopy.Value;

                                        if (elemType.StartsWith("Views"))
                                            continue;

                                        if (elemList.Count == 0)
                                            continue;

                                        ICollection<ElementId> elemIds = ElementTransformUtils.CopyElements(VPlan, elemList, currentViewPlan, null, null);

                                        copiedElements[elemType + $"_{i + 1}"] = elemIds.ToList();
                                    }
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
                            transCreateFloors.Commit();
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



        #endregion






        #region Floor Builder Helper Methods 



        /// <summary>
        /// Check if points are matching
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="digits"></param>
        /// <returns>Bool: True if points match</returns>
        private bool arePointsSimilar(XYZ pt1, XYZ pt2, int digits)
        {
            //Round precision to two spaces after decimal
            Double x1 = Math.Round(pt1.X, digits);
            Double y1 = Math.Round(pt1.Y, digits);
            Double z1 = Math.Round(pt1.X, digits);

            Double x2 = Math.Round(pt2.X, digits);
            Double y2 = Math.Round(pt2.Y, digits);
            Double z2 = Math.Round(pt2.X, digits);

            if (x1 == x2 && y1 == y2 && z1 == z2)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// Copy/Paste duplicate names warning swallower
        /// </summary>
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



        #endregion






        #region Load and Parse JSON File


        /// <summary>
        /// Load JSON File
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
                using (Stacker.GeoJsonClasses.FormViewJsonData frmJsonViewer = new FormViewJsonData(DictJSON))
                {
                    frmJsonViewer.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                TaskDialog.Show("ERROR", ex.Message);
            }


        }



        #endregion





        #region Form Validation

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


        #endregion





        #region Export Images Logic

        private void btnExportImages_Click(object sender, EventArgs e)
        {
            try
            {
                //
                //Select folder path
                //
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Select folder to save plan image file:";
                string selectedPath = "";

                if (fbd.ShowDialog() == DialogResult.OK)
                    selectedPath = fbd.SelectedPath;


                List<View3D> builtMod3DViews = new List<View3D>();
                List<View> allViews = new List<View>();
                List<ViewSheet> allSheets = (new FilteredElementCollector(_doc)).OfClass(typeof(ViewSheet)).OfType<ViewSheet>().ToList();

                Dictionary<string, string> sheetNames = new Dictionary<string, string>();

                //
                //Export images for all Levels created. 
                //
                for (int i = 0; i < AllLevels.Count; i++)
                {
                    Level currentLevel = AllLevels[i];
                    View currentViewPlan = AllViewPlans[i] as View;
                    
                    allViews.Add(currentViewPlan);
                    
                    sheetNames[currentLevel.Name] = $"DB-00{i + 1}";

                    hideElementsFromView(new List<View>() { currentViewPlan });

                    if (selectedPath != "")
                        exportViewPlanImage(currentViewPlan, selectedPath);
                }


                //
                //Create Two 3D Views
                //
                ViewFamilyType viewFamilyType = new FilteredElementCollector(_doc)
                                                    .OfClass(typeof(ViewFamilyType))
                                                    .OfType<ViewFamilyType>()
                                                    .FirstOrDefault(x =>x.ViewFamily == ViewFamily.ThreeDimensional);

                List<View3D> all3DViews = new FilteredElementCollector(_doc).OfClass(typeof(View3D)).OfType<View3D>().ToList();
                List<string> existing3DViewNames = (from v in all3DViews
                                                    select v.Name).ToList();


                using (Transaction transExportImage = new Transaction(_doc))
                {
                    transExportImage.Start($"Mod: Create 3D Views");

                    for (var i = 0; i < 2; i++)
                    {
                        View3D view3DCurrent = (viewFamilyType != null)
                                                ? View3D.CreateIsometric(_doc, viewFamilyType.Id)
                                                : null;

                        string view3DName = $"3D Mod View {i + 1}";

                        if (existing3DViewNames.Contains(view3DName))
                        {
                            builtMod3DViews.Add(view3DCurrent);
                            allViews.Add(view3DCurrent);
                            continue;
                        }

                        view3DCurrent.Name = view3DName;

                        if(i == 0)
                        {
                            var eyeDirection = new XYZ(23.5262711771655, -274.775327941385, 267.088086745375);
                            var upDirection = new XYZ(-0.408248290463863, 0.408248290463863, 0.816496580927726);
                            var forwardDirection = new XYZ(-0.577350269189626, 0.577350269189626, -0.577350269189626);

                            view3DCurrent.SetOrientation(new ViewOrientation3D(eyeDirection, upDirection, forwardDirection));
                            sheetNames[view3DCurrent.Name] = $"DB-30{i + 1}";
                        }
                        else if (i == 1)
                        {
                            var eyeDirection = new XYZ(-462.725440347728, 211.476383583508, 267.088086745375);
                            var upDirection = new XYZ(0.408248290463863, -0.408248290463863, 0.816496580927726);
                            var forwardDirection = new XYZ(0.577350269189626, -0.577350269189626, -0.577350269189626);

                            view3DCurrent.SetOrientation(new ViewOrientation3D(eyeDirection, upDirection, forwardDirection));
                            sheetNames[view3DCurrent.Name] = $"DB-30{i + 1}";
                        }


                        //1.Wireframe
                        //2.Hidden line
                        //3.Shaded
                        //4.Shaded with Edges
                        //5.Consistent Colors
                        //6.Realistic
                        view3DCurrent.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(6);

                        builtMod3DViews.Add(view3DCurrent);
                        allViews.Add(view3DCurrent);

                        ElementsBuilt["Views 3D"] = new List<ElementId>() { view3DCurrent.Id };
                    }


                    transExportImage.Commit();
                }



                //
                //Hide all elements not needed in the 3D View
                //
                for (var i = 0; i < builtMod3DViews.Count; i++)
                {
                    Autodesk.Revit.DB.View current3DView = builtMod3DViews[i] as Autodesk.Revit.DB.View;

                    if (selectedPath != "")
                        hideElementsFromView(new List<View>() { current3DView });
                }



                //
                //Export 3D View Images
                //
                for (var i = 0; i < builtMod3DViews.Count; i++)
                {
                    Autodesk.Revit.DB.View current3DView = builtMod3DViews[i] as Autodesk.Revit.DB.View;

                    if (selectedPath != "")
                        exportViewPlanImage(current3DView, selectedPath);
                }




                //
                //Create and Export Sheet Images
                //
                List<Element> titleBlocks = loadTitleBlocks();
                Element selectedTitleBlock = (from tb in titleBlocks
                                              where tb.Name == "E1 30x42 Horizontal"
                                              select tb).FirstOrDefault();

                Dictionary<string, ViewSheet> builtSheets = new Dictionary<string, ViewSheet>();

                bool moveVPs = true;
                int count = 0;

                foreach (var sht in sheetNames)
                {
                    ViewSheet builtSheet = null;
                    Viewport builtViewPort = null;
                    XYZ centerPtSheet = null;

                    builtSheet = (from s in allSheets
                                    where s.Name == sht.Key
                                    select s).FirstOrDefault();

                    if(builtSheet == null)
                    {
                        builtSheet = createCustomSheet(selectedTitleBlock.Id, sht.Key, sht.Value);
                        builtSheets[sht.Value] = builtSheet;

                        using (Transaction transPlaceViewPort = new Transaction(_doc))
                        {
                            transPlaceViewPort.Start($"Mod: Place ViewPort");

                            //XYZ maxPointSheet = builtSheet.get_BoundingBox(builtSheet).Max;
                            //XYZ minPointSheet = builtSheet.get_BoundingBox(builtSheet).Min;
                            UV maxPointSheet = builtSheet.Outline.Max;
                            UV minPointSheet = builtSheet.Outline.Min;
                            double sheetWidth = ((maxPointSheet.U - 0.5) - (minPointSheet.U));
                            double sheetHeight = ((maxPointSheet.V) - (minPointSheet.V));

                            Double ptX = ((maxPointSheet.U - 0.5) - (minPointSheet.U)) / 2;
                            Double ptY = ((maxPointSheet.V) - (minPointSheet.V)) / 2;
                            Double ptZ = 0;
                            XYZ centerPtOfSheet = new XYZ(ptX, ptY, ptZ);
                            centerPtSheet = centerPtOfSheet;

                            //Scales: 0.125, 0.1875, 0.25, 0.375, 0.5, 0.75, 1
                            List<int> scales = new List<int>() { 96, 64, 48, 32, 24, 16, 12};

                            var currentView = allViews[count];
                            var currentViewWidth = Math.Abs(currentView.Outline.Max.U - currentView.Outline.Min.U);
                            var currentViewHeight = Math.Abs(currentView.Outline.Max.V - currentView.Outline.Min.V);

                            double possibleWidth = 0;
                            double possibleHeight = 0;
                            int usedScale = 96;

                            for (int i = 0; i < scales.Count; i++)
                            {
                                usedScale = scales[i];

                                possibleWidth = (currentViewWidth * (12 / Convert.ToDouble(scales[i]))) / 0.125;
                                possibleHeight = (currentViewHeight * (12 / Convert.ToDouble(scales[i]))) / 0.125;

                                if(possibleWidth > sheetWidth || possibleHeight > sheetHeight)
                                {
                                    if (i == 0)
                                        usedScale = scales[i];
                                    else
                                        usedScale = scales[i - 1];

                                    break;
                                }

                            }


                            Viewport viewPort = Viewport.Create(_doc, builtSheet.Id, allViews[count].Id, centerPtOfSheet);
                            viewPort.LookupParameter("View Scale").Set(usedScale);

                            builtViewPort = viewPort;

                            transPlaceViewPort.Commit();
                        }

                    }

                    count++;



                    //Move built view port to center of sheet space
                    using (Transaction transMoveVPs = new Transaction(_doc))
                    {
                        transMoveVPs.Start($"Mod: Move ViewPort");


                        var maxViewPort = builtViewPort.GetBoxOutline().MaximumPoint;
                        var minViewPort = builtViewPort.GetBoxOutline().MinimumPoint;
                        double viewPortWidth = ((maxViewPort.X) - (minViewPort.X));
                        double viewPortHeight = ((maxViewPort.Y) - (minViewPort.Y));

                        Double ptXvp = (((maxViewPort.X) - (minViewPort.X)) / 2) + minViewPort.X;
                        Double ptYvp = (((maxViewPort.Y) - (minViewPort.Y)) / 2) + minViewPort.Y;
                        Double ptZvp = 0;
                        XYZ pointToInsertvp = new XYZ(ptXvp, ptYvp, ptZvp);

                        double xTrans = centerPtSheet.X - ptXvp;
                        double yTrans = centerPtSheet.Y - ptYvp;
                        double zTrans = centerPtSheet.Z - ptZvp;

                        XYZ translation = new XYZ(xTrans, yTrans, zTrans);

                        ElementTransformUtils.MoveElement(_doc, builtViewPort.Id, translation);

                        transMoveVPs.Commit();

                    }


                    if (builtSheet == null)
                        continue;

                    //Export sheet to image
                    if (selectedPath != "")
                        exportViewPlanImage(builtSheet, selectedPath);
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Exporting View Image  [" + ex.GetType().ToString() + "]. ");
            }

        }



        /// <summary>
        /// Export View images as high quality images to a provided folder path. 
        /// </summary>
        /// <param name="currentViewPlan">View</param>
        /// <param name="selectedPath">Folder Path</param>
        private void exportViewPlanImage(Autodesk.Revit.DB.View currentViewPlan, string selectedPath)
        {
            if (currentViewPlan.IsTemplate || !currentViewPlan.CanBePrinted)
                return;

            using (Transaction transExportImage = new Transaction(_doc))
            {
                transExportImage.Start($"Mod: Export Image");

                IList<ElementId> ImageExportList = new List<ElementId>();
                ImageExportList.Add(currentViewPlan.Id);


                if (selectedPath != "")
                {
                    //Generate date string
                    string dateString = DateTime.Now.ToUniversalTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture);
                    string fileDateString = dateString.Replace(":", "-").Replace(".", "-") + "____" + currentViewPlan.Name;

                    string fullPathAndFileName = selectedPath + @"\" + fileDateString;

                    var imageExportOpt = new ImageExportOptions
                    {
                        ZoomType = ZoomFitType.Zoom,
                        PixelSize = 8192,
                        FilePath = fullPathAndFileName,
                        FitDirection = FitDirectionType.Horizontal,
                        HLRandWFViewsFileType = ImageFileType.JPEGLossless,
                        ImageResolution = ImageResolution.DPI_600,
                        ExportRange = ExportRange.SetOfViews,
                    };

                    imageExportOpt.SetViewsAndSheets(ImageExportList);

                    _doc.ExportImage(imageExportOpt);

                    //DirectoryInfo directory = new DirectoryInfo(selectedPath);
                    //FileInfo imageFile = (from f in directory.GetFiles()
                    //                      orderby f.LastWriteTime descending
                    //                      select f).First();

                    //if (imageFile != null && imageFile.Name.StartsWith(fileDateString))
                    //{
                        //TaskDialog.Show("Image Created", $"Image file from Area ViewPlan: [{currentViewPlan.Name}] created.");
                        //System.Diagnostics.Process.Start(imageFile.FullName);
                    //}


                }

                transExportImage.Commit();

            }

        }


        /// <summary>
        /// Hide all extra elements from the provided view. 
        /// </summary>
        /// <param name="views"></param>
        private void hideElementsFromView(List<Autodesk.Revit.DB.View> views)
        {
            using (Transaction transExportImage = new Transaction(_doc))
            {
                transExportImage.Start($"Mod: Hide View Elems");

                foreach (var view in views)
                {

                    //
                    //Hide all elements not needed in the 3D View
                    //
                    FilteredElementCollector allElementsInView = new FilteredElementCollector(_doc, view.Id);
                    List<Element> elementsInView = allElementsInView.ToElements().ToList();

                    List<ElementId> allElementIdsBuilt = new List<ElementId>();
                    var elementsBuilt = ElementsBuilt.Values.ToList();
                    foreach (List<ElementId> elem in elementsBuilt)
                        allElementIdsBuilt.AddRange(elem);

                    foreach (Element elemInView in elementsInView)
                    {
                        if (elemInView.Category == null)
                            continue;

                        if (elemInView.Category.Name.Equals("Reference Planes")
                          || elemInView.Category.Name.Equals("Elevations")
                          || elemInView.Category.Name.Equals("Views")
                          || elemInView.Category.Name.Equals("Property Lines")
                          || elemInView.Category.Name.Equals("Levels")
                          || elemInView.Category.Name.Equals("Scope Boxes")
                          || elemInView.Category.Name.Equals("Property Lines")
                          || elemInView.Category.Name.Equals("Grids")
                          || !allElementIdsBuilt.Contains(elemInView.Id))
                        {

                            List<ElementId> ids = new List<ElementId>() { elemInView.Id };

                            if (elemInView.CanBeHidden(view))
                            {
                                view.HideElements(ids);
                            }
                        }
                    }
                }

                transExportImage.Commit();

            }
            
        }





        /// <summary>
        /// Loads all title block in the Revit document. 
        /// </summary>
        /// <returns></returns>
        private List<Element> loadTitleBlocks()
        {
            List<Element> titleblockList = new List<Element>();

            try
            {
                FilteredElementCollector collector = new FilteredElementCollector(_doc);
                titleblockList = collector.OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_TitleBlocks).ToElements().ToList();

                if (titleblockList.Count() == 0)
                {
                    MessageBox.Show("Title blocks were not found in current Revit model. ", "Title Blocks not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return titleblockList;
                }

                return titleblockList;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to obtain model Title Block data. " + ex.Message);
            }

        }




        /// <summary>
        /// Build a sheet in Revit
        /// </summary>
        /// <param name="titleblock"></param>
        /// <param name="sheetName"></param>
        /// <param name="sheetNumber"></param>
        /// <returns></returns>
        private ViewSheet createCustomSheet(ElementId titleblock, string sheetName, string sheetNumber)
        {
            using (Transaction transCreateSheets = new Transaction(_doc))
            {
                transCreateSheets.Start("Build Sheet");

                try
                {
                    ViewSheet workingSheet = ViewSheet.Create(_doc, titleblock);
                    workingSheet.Name = sheetName;
                    workingSheet.SheetNumber = sheetNumber;

                    transCreateSheets.Commit();

                    return workingSheet;
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to create new Sheet. " + ex.Message);
                }
            }
        }




        #endregion





        #region Extract Model Data / Export Excel


        List<BldgResult> BuildingResults;





        private void btnExportData_Click(object sender, EventArgs e)
        {

            BuildingResults = new List<BldgResult>();

            int floorCount = 0;

            int studioCount = 0;
            int oneBedCount = 0;
            int twoBedCount = 0;
            int coreCount = 0;
            //
            //Loop through all elements and itemize for Excel export
            //
            foreach (KeyValuePair<string, List<ElementId>> elem in ElementsBuilt)
            {
                string elemCategoryName = elem.Key;
                List<ElementId> elemIDs = elem.Value;
                string currentLevelName = $"LVL_{floorCount}";


                //
                //If elements are Mod Regions
                //
                if (elemCategoryName.Contains("Mod Regions"))
                {

                    foreach (ElementId elemID in elemIDs)
                    {
                        FilledRegion filledRegion = _doc.GetElement(elemID) as FilledRegion;

                        Parameter parComments = filledRegion.LookupParameter("Comments");
                        Parameter parArea = filledRegion.LookupParameter("Area");

                        string comment = parComments.AsString();
                        double area = parArea.AsDouble();


                        if (comment.Contains("MOD_3"))
                        {
                            string catName = $"Floor - Two Bed";

                            BldgResult elemExists = BuildingResults.Where(elem1 => elem1.ElementName == catName && elem1.LevelName == currentLevelName).FirstOrDefault();

                            if (elemExists != null)
                            {
                                double oldArea = elemExists.Quantity;
                                elemExists.Quantity = oldArea + area;
                                twoBedCount++;
                            }
                            else
                            {
                                BldgResult result = new BldgResult();
                                result.CreateBldgResult(currentLevelName, catName, area, "SF", "Filled Region", "AreaElements");

                                BuildingResults.Add(result);
                                twoBedCount++;

                            }

                        }
                        else if (comment.Contains("MOD_2"))
                        {
                            string catName = $"Floor - One Bed";

                            BldgResult elemExists = BuildingResults.Where(elem1 => elem1.ElementName == catName && elem1.LevelName == currentLevelName).FirstOrDefault();

                            if (elemExists != null)
                            {
                                double oldArea = elemExists.Quantity;
                                elemExists.Quantity = oldArea + area;
                                oneBedCount++;
                            }
                            else
                            {
                                BldgResult result = new BldgResult();
                                result.CreateBldgResult(currentLevelName, catName, area, "SF", "Filled Region", "AreaElements");

                                BuildingResults.Add(result);
                                oneBedCount++;

                            }

                        }
                        else if (comment.Contains("MOD_1"))
                        {
                            string catName = $"Floor - Studio";

                            BldgResult elemExists = BuildingResults.Where(elem1 => elem1.ElementName == catName && elem1.LevelName == currentLevelName).FirstOrDefault();

                            if (elemExists != null)
                            {
                                double oldArea = elemExists.Quantity;
                                elemExists.Quantity = oldArea + area;
                                studioCount++;
                            }
                            else
                            {
                                BldgResult result = new BldgResult();
                                result.CreateBldgResult(currentLevelName, catName, area, "SF", "Filled Region", "AreaElements");

                                BuildingResults.Add(result);
                                studioCount++;
                            }


                        }
                        else if (comment.Contains("CORE"))
                        {
                            string catName = $"Floor - Core";

                            BldgResult elemExists = BuildingResults.Where(elem1 => elem1.ElementName == catName && elem1.LevelName == currentLevelName).FirstOrDefault();

                            if (elemExists != null)
                            {
                                double oldArea = elemExists.Quantity;
                                elemExists.Quantity = oldArea + area;
                                coreCount++;
                            }
                            else
                            {
                                BldgResult result = new BldgResult();
                                result.CreateBldgResult(currentLevelName, catName, area, "SF", "Filled Region", "AreaElements");

                                BuildingResults.Add(result);
                                coreCount++;
                            }

                        }

                    }



                    string elementNameStudio = "Unit Count - Studio";
                    BldgResult elemExistsStudio = BuildingResults.Where(elem1 => elem1.ElementName == elementNameStudio && elem1.LevelName == currentLevelName).FirstOrDefault();

                    if (elemExistsStudio != null)
                    {
                        elemExistsStudio.Quantity++;
                    }
                    else
                    {
                        BldgResult resultStudio = new BldgResult();
                        resultStudio.CreateBldgResult(currentLevelName, elementNameStudio, studioCount, "EA", "Custom", "AreaElements");

                        BuildingResults.Add(resultStudio);
                    }


                    string elementNameOneBed = "Unit Count - One Bed";
                    BldgResult elemExistsOneBed = BuildingResults.Where(elem1 => elem1.ElementName == elementNameOneBed && elem1.LevelName == currentLevelName).FirstOrDefault();

                    if (elemExistsStudio != null)
                    {
                        elemExistsOneBed.Quantity++;
                    }
                    else
                    {
                        BldgResult resultOneBed = new BldgResult();
                        resultOneBed.CreateBldgResult(currentLevelName, elementNameOneBed, oneBedCount, "EA", "Custom", "AreaElements");

                        BuildingResults.Add(resultOneBed);
                    }




                    string elementNameTwoBed = "Unit Count - Two Bed";
                    BldgResult elemExistsTwoBed = BuildingResults.Where(elem1 => elem1.ElementName == elementNameTwoBed && elem1.LevelName == currentLevelName).FirstOrDefault();

                    if (elemExistsTwoBed != null)
                    {
                        elemExistsOneBed.Quantity++;
                    }
                    else
                    {
                        BldgResult resultTwoBed = new BldgResult();
                        resultTwoBed.CreateBldgResult(currentLevelName, elementNameTwoBed, twoBedCount, "EA", "Custom", "AreaElements");

                        BuildingResults.Add(resultTwoBed);
                    }



                    string elementNameCore = "Unit Count - Core";
                    BldgResult elemExistsCore = BuildingResults.Where(elem1 => elem1.ElementName == elementNameCore && elem1.LevelName == currentLevelName).FirstOrDefault();

                    if (elemExistsCore != null)
                    {
                        elemExistsCore.Quantity++;
                    }
                    else
                    {
                        BldgResult resultCore = new BldgResult();
                        resultCore.CreateBldgResult(currentLevelName, elementNameCore, twoBedCount, "EA", "Custom", "AreaElements");

                        BuildingResults.Add(resultCore);
                    }



                }
                //
                //If elements are Room Elements
                //
                else if (elemCategoryName.Contains("Room Elements"))
                {

                    foreach (ElementId elemID in elemIDs)
                    {
                        Element roomElem = _doc.GetElement(elemID) as Element;
                        string elementCatName = roomElem.Category.Name;
                        string elementName = roomElem.Name;
                        FamilyInstance roomElemFamilyInstance = roomElem as FamilyInstance;
                        FamilySymbol roomElemFamilySymbol = null;
                        Family roomElemFamily = null;
                        string roomElemFamilyName = "";

                        if (roomElemFamilyInstance != null)
                        {
                            roomElemFamilySymbol = roomElemFamilyInstance.Symbol;
                            if(roomElemFamilySymbol != null)
                                roomElemFamily = roomElemFamilySymbol.Family;
                        }

                        if (roomElemFamily != null)
                            roomElemFamilyName = roomElemFamily.Name;

                        


                        string elementNameForRecording = $"{elementCatName}_{elementName}_{roomElemFamilyName}";

                        var elemExists = BuildingResults.Where(elem1 => elem1.ElementName == elementNameForRecording && elem1.LevelName == currentLevelName).FirstOrDefault();

                        if (elemExists != null)
                        {
                            elemExists.Quantity++;
                        }
                        else
                        {
                            BldgResult result = new BldgResult();
                            result.CreateBldgResult(currentLevelName, elementNameForRecording, 1, "EA", "RoomElements", roomElemFamilyName);

                            BuildingResults.Add(result);
                        }



                        if (elementCatName == "Walls")
                        {
                            Parameter parWallLength = roomElem.LookupParameter("Length");
                            elementNameForRecording = elementNameForRecording + " - Length";

                            double length = parWallLength.AsDouble();

                            var elemExistsWall = BuildingResults.Where(elem1 => elem1.ElementName == elementNameForRecording && elem1.LevelName == currentLevelName).FirstOrDefault();

                            if (elemExistsWall != null)
                            {
                                double oldLength = elemExistsWall.Quantity;
                                elemExistsWall.Quantity = oldLength + length;
                            }
                            else
                            {
                                BldgResult result = new BldgResult();
                                result.CreateBldgResult(currentLevelName, elementNameForRecording, length, "LF", "RoomElements", roomElemFamilyName);

                                BuildingResults.Add(result);

                            }



                            var elemExistsWallTotal = BuildingResults.Where(elem1 => elem1.ElementName.Contains(elementNameForRecording + " WALL SURFACE AREA (TWO SIDED)") && elem1.LevelName == currentLevelName).FirstOrDefault();

                            if (elemExistsWallTotal != null)
                            {
                                double oldLength = elemExistsWallTotal.Quantity;
                                elemExistsWallTotal.Quantity = oldLength + (length * TypFloorHeight * 2);
                            }
                            else
                            {
                                BldgResult resultFacadeSurface = new BldgResult();
                                var elementNameWallSF = elementNameForRecording + " WALL SURFACE AREA (TWO SIDED)";
                                var quantityWallSF = length * TypFloorHeight * 2;
                                resultFacadeSurface.CreateBldgResult(currentLevelName, elementNameWallSF, quantityWallSF, "SF", "Floor", "AreaElements");

                                BuildingResults.Add(resultFacadeSurface);
                            }


                        }



                    }


                }
                else if (elemCategoryName.Contains("Floor"))
                {

                    foreach (ElementId elemID in elemIDs)
                    {
                        Floor floorElement = _doc.GetElement(elemID) as Floor;

                        if (floorElement == null)
                            continue;

                        Parameter parComments = floorElement.LookupParameter("Comments");
                        Parameter parArea = floorElement.LookupParameter("Area");
                        Parameter parPerimeter = floorElement.LookupParameter("Perimeter");

                        string comment = parComments.AsString();
                        double area = parArea.AsDouble();
                        double perimeter = parPerimeter.AsDouble();

                        string catName = $"Floor - TOTAL";
                        if(floorCount == TotalFloors - 1)
                            catName = $"Floor - Roof - TOTAL";

                        BldgResult elemExists = BuildingResults.Where(elem1 => elem1.ElementName == catName && elem1.LevelName == currentLevelName).FirstOrDefault();

                        if (elemExists != null)
                        {
                            double oldArea = elemExists.Quantity;
                            elemExists.Quantity = oldArea + area;
                        }
                        else
                        {
                            BldgResult result = new BldgResult();
                            result.CreateBldgResult(currentLevelName, catName + " AREA", area, "SF", "Floor", "AreaElements");

                            BuildingResults.Add(result);



                            BldgResult resultPerimeter = new BldgResult();
                            resultPerimeter.CreateBldgResult(currentLevelName, catName + " PERIMETER", perimeter, "LF", "Floor", "AreaElements");

                            BuildingResults.Add(resultPerimeter);



                            BldgResult resultFacadeSurface = new BldgResult();
                            resultFacadeSurface.CreateBldgResult(currentLevelName, catName + " FACADE SURFACE AREA", perimeter * TypFloorHeight, "SF", "Floor", "AreaElements");

                            BuildingResults.Add(resultFacadeSurface);
                        }


                    }
                }
                else if (elemCategoryName.Contains("Hallway Elements"))
                {

                    foreach (ElementId elemID in elemIDs)
                    {
                        Floor floorElement = _doc.GetElement(elemID) as Floor;

                        if (floorElement == null)
                            continue;

                        Parameter parComments = floorElement.LookupParameter("Comments");
                        Parameter parArea = floorElement.LookupParameter("Area");

                        string comment = parComments.AsString();
                        double area = parArea.AsDouble();

                        string catName = $"Floor - Hallway";

                        BldgResult elemExists = BuildingResults.Where(elem1 => elem1.ElementName == catName && elem1.LevelName == currentLevelName).FirstOrDefault();

                        if (elemExists != null)
                        {
                            double oldArea = elemExists.Quantity;
                            elemExists.Quantity = oldArea + area;
                        }
                        else
                        {
                            BldgResult result = new BldgResult();
                            result.CreateBldgResult(currentLevelName, catName, area, "SF", "Floor", "AreaElements");

                            BuildingResults.Add(result);
                        }
                    }


                    floorCount++;

                    studioCount = 0;
                    oneBedCount = 0;
                    twoBedCount = 0;
                    coreCount = 0;
                }
                


            }




            //
            //Caclulate Totals
            //

            List<BldgResult> categoryTotals = new List<BldgResult>(); ;

            foreach (var bldg in BuildingResults)
            {
                if (bldg.ElementName.Contains("Windows"))
                {
                    BldgResult elemExistsWin = categoryTotals.Where(elem1 => elem1.ElementName == "Windows-ALL").FirstOrDefault();

                    if (elemExistsWin != null)
                    {
                        double oldQty = elemExistsWin.Quantity;
                        elemExistsWin.Quantity = oldQty + bldg.Quantity;
                    }
                    else
                    {
                        BldgResult resultWinAll = new BldgResult();
                        resultWinAll.CreateBldgResult("TOTAL", "Windows-ALL", bldg.Quantity, bldg.UnitType, bldg.FamilyName, bldg.CategoryType);

                        categoryTotals.Add(resultWinAll);
                    }
                }
                else if (bldg.ElementName.Contains("Doors"))
                {
                    BldgResult elemExistsDoor = categoryTotals.Where(elem1 => elem1.ElementName == "Doors-ALL").FirstOrDefault();

                    if (elemExistsDoor != null)
                    {
                        double oldQty = elemExistsDoor.Quantity;
                        elemExistsDoor.Quantity = oldQty + bldg.Quantity;
                    }
                    else
                    {
                        BldgResult resultWinAll = new BldgResult();
                        resultWinAll.CreateBldgResult("TOTAL", "Doors-ALL", bldg.Quantity, bldg.UnitType, bldg.FamilyName, bldg.CategoryType);

                        categoryTotals.Add(resultWinAll);
                    }

                }
                else if (bldg.ElementName.Contains("Unit Count") && !bldg.ElementName.Contains("Core"))
                {
                    BldgResult elemExistsUnits = categoryTotals.Where(elem1 => elem1.ElementName == "Units-ALL").FirstOrDefault();

                    if (elemExistsUnits != null)
                    {
                        double oldQty = elemExistsUnits.Quantity;
                        elemExistsUnits.Quantity = oldQty + bldg.Quantity;
                    }
                    else
                    {
                        BldgResult resultWinAll = new BldgResult();
                        resultWinAll.CreateBldgResult("TOTAL", "Units-ALL", bldg.Quantity, bldg.UnitType, bldg.FamilyName, bldg.CategoryType);

                        categoryTotals.Add(resultWinAll);
                    }

                }
            }

            BuildingResults.AddRange(categoryTotals);
            List<BldgResult> sortedBldgResults = BuildingResults.OrderBy(x => x.ElementName).ToList();
            List<BldgResult> totalBldgResults = new List<BldgResult>();

            foreach(var bldg in sortedBldgResults)
            {
                BldgResult elemExists = totalBldgResults.Where(elem1 => elem1.ElementName == "TOTAL_" + bldg.ElementName).FirstOrDefault();

                if (elemExists != null)
                {
                    double oldQty = elemExists.Quantity;
                    elemExists.Quantity = oldQty + bldg.Quantity;
                }
                else
                {
                    BldgResult result = new BldgResult();
                    result.CreateBldgResult("TOTAL", "TOTAL_" + bldg.ElementName, bldg.Quantity, bldg.UnitType, bldg.FamilyName, bldg.CategoryType);

                    totalBldgResults.Add(result);
                }
            }


            DataGridView newDGV = new DataGridView();
            var column1 = new DataGridViewTextBoxColumn();
            var column2 = new DataGridViewTextBoxColumn();
            var column3 = new DataGridViewTextBoxColumn();
            var column4 = new DataGridViewTextBoxColumn();
            var column5 = new DataGridViewTextBoxColumn();
            var column6 = new DataGridViewTextBoxColumn();

            column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            column1.HeaderText = "Revit Floor";
            column1.MinimumWidth = 6;
            column1.Name = "Column1";
            column1.ReadOnly = true;

            column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            column2.HeaderText = "Revit Element";
            column2.MinimumWidth = 6;
            column2.Name = "Column2";
            column2.ReadOnly = true;

            column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            column3.HeaderText = "Quantity";
            column3.MinimumWidth = 6;
            column3.Name = "Column3";
            column3.ReadOnly = true;
            column3.DefaultCellStyle.Format = "N2";

            column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            column4.HeaderText = "Unit";
            column4.MinimumWidth = 6;
            column4.Name = "column4";
            column4.ReadOnly = true;

            column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            column5.HeaderText = "Category";
            column5.MinimumWidth = 6;
            column5.Name = "column5";
            column5.ReadOnly = true;

            column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            column6.HeaderText = "Family";
            column6.MinimumWidth = 6;
            column6.Name = "column6";
            column6.ReadOnly = true;

            newDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { column1, column2, column3, column4, column5, column6 });


            int n = 0;
            for (int i = 0; i <= floorCount; i++)
            {
                string lvl = $"LVL_{i}";

                n = newDGV.Rows.Add();
                newDGV.Rows[n].Cells[0].Value = $"-----{lvl}-----";

                foreach (BldgResult elem in sortedBldgResults)
                {
                    if (!elem.LevelName.Contains(lvl))
                        continue;

                    n = newDGV.Rows.Add();

                    newDGV.Rows[n].Cells[0].Value = lvl;
                    newDGV.Rows[n].Cells[1].Value = elem.ElementName;
                    newDGV.Rows[n].Cells[2].Value = elem.Quantity;
                    newDGV.Rows[n].Cells[3].Value = elem.UnitType;
                    newDGV.Rows[n].Cells[4].Value = elem.CategoryType;
                    newDGV.Rows[n].Cells[5].Value = elem.FamilyName;
                }
            }


            for (int j = 0; j <= 1; j++)
            {
                n = newDGV.Rows.Add();

                newDGV.Rows[n].Cells[0].Value = "";
            }

            n = newDGV.Rows.Add();
            newDGV.Rows[n].Cells[0].Value = "-----TOTAL VALUES------";

            foreach (BldgResult elem in totalBldgResults)
            {
                n = newDGV.Rows.Add();

                newDGV.Rows[n].Cells[0].Value = "";
                newDGV.Rows[n].Cells[1].Value = elem.ElementName;
                newDGV.Rows[n].Cells[2].Value = elem.Quantity;
                newDGV.Rows[n].Cells[3].Value = elem.UnitType;
                newDGV.Rows[n].Cells[4].Value = elem.CategoryType;
                newDGV.Rows[n].Cells[5].Value = elem.FamilyName;
            }




            SaveFileDialog saveResults = new SaveFileDialog();

            saveResults.Filter = "Excel File|*.xlsx";
            saveResults.Title = "Export table";
            saveResults.ShowDialog();

            if (saveResults.FileName != "")
            {
                DataTable sheetListResult = null;

                sheetListResult = CreateDataTable(newDGV, _doc);

                //Save
                SaveExcelWorksheet(sheetListResult, true, saveResults.FileName, "Building Data Output");

                var responsetoOpen = MessageBox.Show($"Building Data Output '{saveResults.FileName.ToString()}' successfully exported to Excel.\r\n\r\nOpen Excel file?", "Excel Export", MessageBoxButtons.YesNo);

                //Open file
                if (responsetoOpen == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveResults.FileName);
                }

                return;

            }


        }


        /// <summary>
        /// Save a Datatable to an Excel Open XML file
        /// </summary>
        /// <param name="inputData">Datatable to save</param>
        /// <param name="saveHeaders">Save headers in top row?</param>
        /// <param name="path">Destination file path (*.xlsx)</param>
        /// <param name="worksheetName">Excel worksheet name</param>
        /// <param name="headerBold">Make header format bold?</param>
        /// <param name="appendToExisting">If false, a new file will be created, if not, a sheet with the input worksheetName will be added</param>
        public static void SaveExcelWorksheet(DataTable inputData, bool saveHeaders, string path, string worksheetName = "Worksheet 1", bool headerBold = true, bool appendToExisting = false)
        {
            DataTableToExcel(inputData, saveHeaders, path, worksheetName, headerBold, appendToExisting);
        }



        /// <summary>
        /// Convert object value to double and converts to specificed decimat numbers. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        private static double getDouble(object value, int round = 2)
        {
            if (value == null)
            {
                return 0;
            }
            else if (value is double)
            {
                var dblValue = Convert.ToDouble(value);
                var rounded = Math.Round(dblValue, round);
                return rounded;
            }
            else if (value is string)
            {
                var dblValue = 0.0;
                Double.TryParse((string)value, out dblValue);
                var rounded = Math.Round(dblValue, round);

                return rounded;
            }

            return 0;

        }


        /// <summary>
        /// Save a Datatable to an excel file. Set Excel sheet style. 
        /// </summary>
        /// <param name="inputData">Datatable to save</param>
        /// <param name="saveHeaders">Save headers in top row?</param>
        /// <param name="path">Destination file path (*.xlsx)</param>
        /// <param name="worksheetName">Excel worksheet name</param>
        /// <param name="headerBold">Make header format bold?</param>
        /// <param name="appendToExisting">If false, a new file will be created, if not, a sheet with the input worksheetName will be added</param>
        public static void DataTableToExcel(DataTable inputData, bool saveHeaders, string path, string worksheetName = "Worksheet 1", bool headerBold = true, bool appendToExisting = false)
        {
            if (appendToExisting == false)
            {
                System.IO.File.Delete(path);
            }

            using (EPPlus.ExcelPackage pck = new EPPlus.ExcelPackage(new System.IO.FileInfo(path)))
            {
                EPPlus.ExcelWorksheet ws = pck.Workbook.Worksheets.Add(worksheetName);
                ws.Cells["A1"].LoadFromDataTable(inputData, saveHeaders);
                int totalRows = inputData.Rows.Count + 50;

                using (var rng = ws.Cells[$"B1:B{totalRows}"])
                {
                    rng.AutoFitColumns();

                    foreach (var r in rng)
                    {
                        if (r.Value != null)
                        {
                            var str = r.Value as string;
                            if (str != null && str.Contains("-ALL"))
                                r.Style.HorizontalAlignment = EPPlus.Style.ExcelHorizontalAlignment.Right;
                        }
                    }
                }


                using (var rng = ws.Cells[$"C1:C{totalRows}"])
                {
                    rng.AutoFitColumns();
                    rng.Style.Numberformat.Format = "0.00";

                    foreach(var r in rng)
                    {
                        var val = getDouble(r.Value);
                        if(val != 0)
                            r.Value = val;
                    }
                }


                using (var rng = ws.Cells[$"D1:D{totalRows}"])
                    rng.AutoFitColumns();

                using (var rng = ws.Cells[$"E1:E{totalRows}"])
                    rng.AutoFitColumns();

                using (var rng = ws.Cells[$"F1:F{totalRows}"])
                    rng.AutoFitColumns();

                using (var rng = ws.Cells["A3:F3"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Font.UnderLine = true;
                    rng.Style.VerticalAlignment = EPPlus.Style.ExcelVerticalAlignment.Center;
                    rng.Style.HorizontalAlignment = EPPlus.Style.ExcelHorizontalAlignment.Center;
                }

                using (var rng = ws.Cells["A1:F1"])
                {
                    rng.Merge = true;
                    rng.Style.HorizontalAlignment = EPPlus.Style.ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = EPPlus.Style.ExcelVerticalAlignment.Center;
                }


                if (headerBold && saveHeaders)
                    ws.Row(1).Style.Font.Bold = true;

                pck.Save();
            }
        }



        /// <summary>
        /// Format excel sheet consisting of column data
        /// </summary>
        /// <param name="d"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static DataTable CreateDataTable(DataGridView d, Document doc)
        {
            DataTable buildingDataOutput = new DataTable();

            //Column headers to be left empty
            buildingDataOutput.Columns.Add("BUILDING DATA OUTPUT", typeof(string));
            buildingDataOutput.Columns.Add("  ", typeof(string));
            buildingDataOutput.Columns.Add("   ", typeof(string));
            buildingDataOutput.Columns.Add("    ", typeof(string));
            buildingDataOutput.Columns.Add("     ", typeof(string));
            buildingDataOutput.Columns.Add("      ", typeof(string));



            //Second excel row also kept empty
            buildingDataOutput.Rows.Add(
                $"",
                "",
                "",
                "",
                "",
                "");

            //Add column headers in 3rd row to contain title
            buildingDataOutput.Rows.Add(
                "Floor",
                "Element Name",
                "Element Quantity",
                "Units",
                "Category",
                "Family Type");


            //Add building data in 4th row
            foreach (DataGridViewRow row in d.Rows)
            {
                var col0 = row.Cells[0].Value == null ? "" : row.Cells[0].Value;
                var col1 = row.Cells[1].Value == null ? "" : row.Cells[1].Value;
                var col2 = row.Cells[2].Value == null ? "" : row.Cells[2].Value;
                var col3 = row.Cells[3].Value == null ? "" : row.Cells[3].Value;
                var col4 = row.Cells[4].Value == null ? "" : row.Cells[4].Value;
                var col5 = row.Cells[5].Value == null ? "" : row.Cells[5].Value;


                buildingDataOutput.Rows.Add(
                    col0,
                    col1,
                    col2,
                    col3,
                    col4,
                    col5);

            }

            //Do not sort at the moment
            //sheetListResult.DefaultView.Sort = "[Reference Level] asc";
            buildingDataOutput = buildingDataOutput.DefaultView.ToTable();

            return buildingDataOutput;
        }



        #endregion


        #region "GeoJSON and Zoning API"

        public object QueryResultRegrid { get; set; }
        public string JsonRegrid { get; set; }
        public object QueryResultZoneomics { get; set; }
        public string JsonZoneomics { get; set; }


        private void btnDrawGeoJSONData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = null;
            GeoJsonParser geoJsonParser = null;
            GeoJsonResultCollection geoJason = null;

            if (cbLoadJSONFile.Checked)
            {
                openFileDialog = new OpenFileDialog() { Filter = "Json file|*.json", Title = "Select GeoJson file" };

                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;

                geoJsonParser = new GeoJsonParser();

                geoJason = geoJsonParser.ParseFile(openFileDialog.FileName);

            }
            else
            {
                if(JsonRegrid == null)
                {
                    TaskDialog.Show("Error!", "GeoJson file not found.");
                    return;
                }

                geoJsonParser = new GeoJsonParser();
                var deserializeJSON = JsonConvert.DeserializeObject(JsonRegrid).ToString();
                //geoJason = geoJsonParser.ParseJSON(deserializeJSON);
                geoJason = geoJsonParser.ParseJSON(deserializeJSON);
            }



            if (geoJason is null)
            {
                TaskDialog.Show("Error!", "Error reading GeoJson file.");

                return;
            }


            using (Transaction transaction = new Transaction(_doc))
            {
                if (transaction.Start("MOD: Create GeoJson Polygon") == TransactionStatus.Started)
                {
                    foreach (var result in geoJason.Results)
                    {
                        if (result.Geometry is null)
                        {
                            continue;
                        }

                        var isFirstPolygonSet = true;
                        var basePoint = XYZ.Zero;
                        var factor = 0.0;

                        foreach (var polygonSet in result.Geometry.PolygonsSet)
                        {
                            var profileloops = new List<CurveLoop>();

                            foreach (var polygon in polygonSet)
                            {

                                XYZ currentPoint = null;

                                var profileloop = new CurveLoop();
                                var isFirstCoordinate = true;

                                foreach (var coordinate in polygon.Coordinates)
                                {
                                    if (isFirstPolygonSet)
                                    {
                                        factor = Math.Abs(Math.Cos(DegreesToRadians(coordinate.latitude)));
                                    }

                                    var nextPoint = ConvertCoordinateToXYZ(coordinate.longitude, coordinate.latitude, factor);

                                    if (isFirstPolygonSet)
                                    {
                                        basePoint = nextPoint;
                                        isFirstPolygonSet = false;
                                    }

                                    if (isFirstCoordinate)
                                    {
                                        currentPoint = nextPoint;
                                        isFirstCoordinate = false;

                                        continue;
                                    }

                                    var line = Line.CreateBound(currentPoint.Subtract(basePoint), nextPoint.Subtract(basePoint));

                                    profileloop.Append(line);

                                    currentPoint = nextPoint;
                                }

                                profileloops.Add(profileloop);

                                var filteredElementCollector = new FilteredElementCollector(_doc).OfClass(typeof(FilledRegionType));
                                var filledRegionPattern = filteredElementCollector.Cast<FilledRegionType>().Where(region => region.Name.Equals("Solid Black"));
                                var filledRegion = FilledRegion.Create(_doc, filledRegionPattern.FirstOrDefault().Id,
                                    _doc.ActiveView.Id, profileloops);

                                _doc.Regenerate();

                                var uiDocument = _uidoc;
                                var selectedCollection = new ElementId[] { filledRegion.Id };

                                uiDocument.Selection.SetElementIds(selectedCollection);
                                uiDocument.ShowElements(selectedCollection);
                                uiDocument.RefreshActiveView();

                                var area = filledRegion.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED).AsValueString();
                                var boundingBox = filledRegion.get_BoundingBox(_doc.ActiveView);
                                var boundingBoxWidth = Math.Round(boundingBox.Max.X - boundingBox.Min.X, 3);
                                var boundingBoxHeight = Math.Round(boundingBox.Max.Y - boundingBox.Min.Y, 3);
                                var boundingBoxDiagonal = Math.Round(Math.Sqrt(Math.Pow(boundingBoxHeight, 2) + Math.Pow(boundingBoxWidth, 2)), 3);
                                var summary = $"Area = {area}\nBounding Box Width = {boundingBoxWidth} ft\nBounding Box Height = {boundingBoxHeight} ft\n"
                                    + $"Bounding Box Diagonal = {boundingBoxDiagonal} ft";

                                TaskDialog.Show("Summary", summary);
                            }
                        }
                    }

                    transaction.Commit();
                }
            }


        }


        /// <summary>
        /// The ConvertCoordinateToXYZ.
        /// </summary>
        /// <param name="longitude">The longitude<see cref="double"/>.</param>
        /// <param name="latitude">The latitude<see cref="double"/>.</param>
        /// <returns>The <see cref="XYZ"/>.</returns>
        private XYZ ConvertCoordinateToXYZCoordinateSharp(double longitude, double latitude)
        {
            var coordinate = new CoordinateSharp.Coordinate(latitude, longitude);

            var x = coordinate.UTM.Easting * 3.281;
            var y = coordinate.UTM.Northing * 3.281;

            return new XYZ(x, y, 0);
        }


        /// <summary>
        /// The ConvertCoordinateToXYZ.
        /// </summary>
        /// <param name="longitude">The longitude<see cref="double"/>.</param>
        /// <param name="latitude">The latitude<see cref="double"/>.</param>
        /// <param name="factor">The factor<see cref="double"/>.</param>
        /// <returns>The <see cref="XYZ"/>.</returns>
        private XYZ ConvertCoordinateToXYZ(double longitude, double latitude, double factor)
        {
            var EarthRadius = 6378137 * 3.281 * factor;
            var x = EarthRadius * DegreesToRadians(longitude);
            var y = EarthRadius * Math.Log((Math.Sin(DegreesToRadians(latitude)) + 1) / Math.Cos(DegreesToRadians(latitude)));

            return new XYZ(x, y, 0);
        }

        /// <summary>
        /// The DegreesToRadians.
        /// </summary>
        /// <param name="val">The val<see cref="double"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        private double DegreesToRadians(double val)
        {
            return val * Math.PI / 180;
        }



        private void btnAPIData_Click(object sender, EventArgs e)
        {
            try
            {
                //Pop-out new form to display beam list
                using (GeoZoningForm frmGeoZoning = new GeoZoningForm())
                {
                    frmGeoZoning.ShowDialog();

                    QueryResultRegrid = frmGeoZoning.QueryResultRegrid;
                    JsonRegrid = frmGeoZoning.JsonRegrid;
                    QueryResultZoneomics = frmGeoZoning.QueryResultZoneomics;
                    JsonZoneomics = frmGeoZoning.JsonZoneomics;
                }

            }
            catch (Exception ex)
            {
                TaskDialog.Show("ERROR", ex.Message);
            }
        }

        #endregion



    }
}
