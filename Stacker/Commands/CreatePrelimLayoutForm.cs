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

        private Document _doc;
        private UIDocument _uidoc;

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





        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplyUnitPriority_Click(object sender, EventArgs e)
        {
            PriorityStudio = cbStudioPriority.SelectedIndex;
            Priority1Bed = cb1BedPriority.SelectedIndex;
            Priority2Bed = cb2BedPriority.SelectedIndex;

            double totalSF = FloorOverallSquareFootage;
            double totalSFForUnits = FloorOverallSquareFootage - (FloorHallwayWidth * FloorOverallLength);

        }



        bool levelBuilt = false;
        Dictionary<string, List<ElementId>> elementsBuilt = new Dictionary<string, List<ElementId>>();
        Level level = null;
        ViewPlan vplan = null;

        private void btnDeleteGeom_Click(object sender, EventArgs e)
        {

            if (elementsBuilt.Count > 0)
            {
                using (var transDeleteOldMods = new Transaction(_doc, "Mod: Delete Old Mods"))
                {
                    transDeleteOldMods.Start();

                    foreach (var elemCat in elementsBuilt)
                    {
                        foreach (var elem in elemCat.Value)
                        {
                            _doc.Delete(elem);
                        }
                    }

                    elementsBuilt = new Dictionary<string, List<ElementId>>();

                    transDeleteOldMods.Commit();
                }
            }
        }


        /// <summary>
        /// 
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

                double fixedModWidth = Convert.ToDouble(btnFixedWidth.Text);

                // The elevation to apply to the new level
                double elevation = 20.0;

                // Begin to create a level
                List<Level> levels = new FilteredElementCollector(_doc).OfClass(typeof(Level)).Cast<Level>().ToList();


                //Obtain the View Template Architectural - FloorPlan
                ViewFamilyType structuralvft = new FilteredElementCollector(_doc).OfClass(typeof(ViewFamilyType))
                    .Cast<ViewFamilyType>()
                    .FirstOrDefault<ViewFamilyType>(x => ViewFamily.FloorPlan == x.ViewFamily);

                

                if (!levelBuilt)
                {
                    using (var transBuildLevels = new Transaction(_doc, "Mod: Build Levels"))
                    {
                        transBuildLevels.Start();

                        level = Level.Create(_doc, elevation);

                        if (null == level)
                            throw new Exception("Create a new level failed.");

                        // Change the level name
                        level.Name = "Mod Level 1";

                        //Create a New View
                        vplan = ViewPlan.Create(_doc, structuralvft.Id, level.Id);
                        vplan.Name = level.Name + " - TEST";

                        levelBuilt = true;

                        

                        transBuildLevels.Commit();
                    }
                }


                if(_doc.ActiveView != vplan)
                {
                    _uidoc.ActiveView = vplan;
                }
                



                if (elementsBuilt.Count > 0)
                {
                    using (var transDeleteOldMods = new Transaction(_doc, "Mod: Delete Old Mods"))
                    {
                        transDeleteOldMods.Start();

                        foreach(var elemCat in elementsBuilt)
                        {
                            foreach(var elem in elemCat.Value)
                            {
                                _doc.Delete(elem);
                            }
                        }

                        elementsBuilt = new Dictionary<string, List<ElementId>>();

                        transDeleteOldMods.Commit();
                    }
                }



                FloorLayout floorLayout = new FloorLayout(FloorOverallLength, FloorOverallWidth, FloorHallwayWidth);

                List<XYPosition> floorEdgePoints = floorLayout.OverallFloorPoints;
                List<List<XYPosition>> floorHallwayPoints = floorLayout.InternalHallwayPoints;
                double modIdealLength = floorLayout.IdealModLength;
                FloorLayout.ModStackType floorScheme = floorLayout.FloorModStackScheme;

                List<Line> floorEdgeLines = new List<Line>();
                CurveArray floorEdgeCurveArray = createCurves(floorEdgePoints, elevation, out floorEdgeLines);


                List<Line> hallwayLines = new List<Line>();

                foreach(List<XYPosition> hallway in floorHallwayPoints)
                {
                    List<Line> hallwayLine = new List<Line>();
                    createCurves(hallway, elevation, out hallwayLine);
                    hallwayLines.AddRange(hallwayLine);
                }





                // Get a floor type for floor creation
                FilteredElementCollector collector = new FilteredElementCollector(_doc);
                collector.OfClass(typeof(FloorType));

                FloorType floorType = collector.FirstElement() as FloorType;

                // The normal vector (0,0,1) that must be perpendicular to the profile.
                XYZ normal = XYZ.BasisZ;

                using (var transCreateFloorView = new Transaction(_doc, "Mod: Create Floor View"))
                {
                    transCreateFloorView.Start();

                    Floor newFloor = _doc.Create.NewFloor(floorEdgeCurveArray, floorType, level, true, normal);
                    elementsBuilt["Floor"] = new List<ElementId>() { newFloor.Id };

                    transCreateFloorView.Commit();
                }







                WallType wType = new FilteredElementCollector(_doc).OfClass(typeof(WallType))
                                    .Cast<WallType>().FirstOrDefault();


                using (var transCreatewalls = new Transaction(_doc, "Mod: Create Walls"))
                {
                    transCreatewalls.Start();

                    var wallsBuilt = new List<ElementId>();

                    foreach (var line in hallwayLines)
                    {
                        var wall = Wall.Create(_doc, line, wType.Id, level.Id, 10, 0, false, true);
                        wall.WallType = wType;

                        wallsBuilt.Add(wall.Id);
                    }

                    foreach (var line in floorEdgeLines)
                    {
                        var wall = Wall.Create(_doc, line, wType.Id, level.Id, 10, 0, false, true);

                        wallsBuilt.Add(wall.Id);
                    }

                    elementsBuilt["Walls - Primary"] = wallsBuilt;


                    transCreatewalls.Commit();
                }



                ModBase modBaseStudio = new ModBase("Studio", 1, PodWidthMax, PodWidthMin, PodLengthMax, PodLengthMin, 10);
                ModBase modBaseOneBed = new ModBase("OneBed", 2, PodWidthMax, PodWidthMin, PodLengthMax, PodLengthMin, 10);
                ModBase modBaseTwoBed = new ModBase("TwoBed", 3, PodWidthMax, PodWidthMin, PodLengthMax, PodLengthMin, 10);

                Dictionary<double, ModOption> optionsStudio = new Dictionary<double, ModOption>();
                Dictionary<double, ModOption> optionsOneBed = new Dictionary<double, ModOption>();
                Dictionary<double, ModOption> optionsTwoBed = new Dictionary<double, ModOption>();


                for (double i = PodWidthMin; i <= PodWidthMax; i++)
                {
                    ModOption studio = new ModOption(i, modIdealLength, modBaseStudio);
                    ModOption oneBed = new ModOption(i, modIdealLength, modBaseOneBed);
                    ModOption twoBed = new ModOption(i, modIdealLength, modBaseTwoBed);

                    optionsStudio[i] = studio;
                    optionsOneBed[i] = oneBed;
                    optionsTwoBed[i] = twoBed;
                }




                List<FloorModBlock> floorBlockOptions = new List<FloorModBlock>();
                int floorBlockCount = 0;

                foreach(var block in floorLayout.ModBlockBasePt)
                {
                    XYPosition currentBlockBasePt = block.Value;
                    int count = 0;

                    FloorModBlock currentBlockOption = new FloorModBlock($"{floorBlockCount.ToString()} - {count.ToString()}", currentBlockBasePt, floorLayout.OverallFloorWidth, floorLayout.OverallFloorLength);

                    while (currentBlockOption.ValidateBlockAdd(optionsTwoBed[fixedModWidth]))
                    {
                        currentBlockOption.AddBlock(optionsTwoBed[fixedModWidth]);
                        count++;
                    }

                    while (currentBlockOption.ValidateBlockAdd(optionsOneBed[fixedModWidth]))
                    {
                        currentBlockOption.AddBlock(optionsOneBed[fixedModWidth]);
                        count++;
                    }

                    while (currentBlockOption.ValidateBlockAdd(optionsStudio[fixedModWidth]))
                    {
                        currentBlockOption.AddBlock(optionsStudio[fixedModWidth]);
                        count++;
                    }


                    floorBlockOptions.Add(currentBlockOption);
                    floorBlockCount++;
                }






                using (var createRegion = new Transaction(_doc, "Mod: Create Region"))
                {
                    createRegion.Start();

                    FilteredElementCollector fillRegionTypes = new FilteredElementCollector(_doc).OfClass(typeof(FilledRegionType));

                    IEnumerable<FilledRegionType> myPatterns = from pattern in fillRegionTypes.Cast<FilledRegionType>()
                                                               where pattern.Name.Equals("Diagonal Crosshatch")
                                                               select pattern;

                    FilledRegionType diagonalPattern = (from pattern in fillRegionTypes.Cast<FilledRegionType>()
                                                        where pattern.Name.Equals("Diagonal Crosshatch")
                                                        select pattern).First();

                    FilledRegionType solidPattern = (from pattern in fillRegionTypes.Cast<FilledRegionType>()
                                                        where pattern.Name.Equals("Diagonal Crosshatch")
                                                        select pattern).First();

                    //FillPatternElement diagonalPattern = (from pattern in fillRegionTypes.Cast<FillPatternElement>()
                    //                                   where pattern.Name.Equals("Diagonal Crosshatch")
                    //                                   select pattern).First();

                    //FillPatternElement solidPattern = (from pattern in fillRegionTypes.Cast<FillPatternElement>()
                    //                                      where pattern.Name.Equals("Solid")
                    //                                      select pattern).First();


                    FilledRegionType newPattern0 = solidPattern.Duplicate("BedStudio") as FilledRegionType;
                    FilledRegionType newPattern1 = solidPattern.Duplicate("BedOne") as FilledRegionType;
                    FilledRegionType newPattern2 = solidPattern.Duplicate("BedTwo") as FilledRegionType;

                    newPattern0.BackgroundPatternColor = new Autodesk.Revit.DB.Color(245, 194, 66); // Orange
                    newPattern1.BackgroundPatternColor = new Autodesk.Revit.DB.Color(108, 245, 66); // Green
                    newPattern2.BackgroundPatternColor = new Autodesk.Revit.DB.Color(66, 245, 239); // Blue

                    newPattern0.BackgroundPatternId = solidPattern.Id;
                    newPattern1.BackgroundPatternId = solidPattern.Id;
                    newPattern2.BackgroundPatternId = solidPattern.Id;


                    int count = fillRegionTypes.Count();
                    var regionsBuilt = new List<ElementId>();

                    foreach (FloorModBlock blk in floorBlockOptions)
                    {
                        for (var i = 0; i < blk.PlacedMods.Count; i++)
                        {
                            List<CurveLoop> profilelps = new List<CurveLoop>();

                            ModOption currentMod = blk.PlacedMods[i];
                            XYPosition currentModBasePos = blk.PlacedModsBasePosition[i];

                            List<XYPosition> currentModGlobalPoints = blk.PlacedModsOuterPosition[i];

                            List<Line> lines;

                            XYPosition pt1 = currentModGlobalPoints[0];
                            XYPosition pt2 = currentModGlobalPoints[1];
                            XYPosition pt3 = currentModGlobalPoints[2];
                            XYPosition pt4 = currentModGlobalPoints[3];

                            CurveLoop profilelp = createCurveLoop(currentModGlobalPoints, elevation, out lines);

                            profilelps.Add(profilelp);

                            FilledRegion filledRegion = FilledRegion.Create(_doc, fillRegionTypes.FirstElementId(), vplan.Id, profilelps);

                            regionsBuilt.Add(filledRegion.Id);
                        }
                    }

                    elementsBuilt["Mod Regions"] = regionsBuilt;

                    createRegion.Commit();
                }

                List<UIView> openViews = _uidoc.GetOpenUIViews().ToList();
                foreach (var v in openViews)
                {
                    if (v.ViewId == vplan.Id)
                    {
                        v.ZoomToFit();
                    }
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                TaskDialog.Show("Error", message);
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



        /// <summary>
        /// Close active form. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            CreatePrelimLayoutForm.ActiveForm.Close();
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
                using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files (*.xls; *.xlsx *.xlsm)|*.xls;*.xlsx;*.xlsm" })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (openFileDialog.FileName != null)
                        {


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("ERROR", ex.Message);
            }
        }


    }
}
