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

        public CreatePrelimLayoutForm(Document doc)
        {
            InitializeComponent();

            _doc = doc;

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

            //Determine if 2 Tier or 1 Tier
                //Place Hallway
                    //Middle Hallway
                    //Edge Hallway
            
            
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

                // The elevation to apply to the new level
                double elevation = 20.0;

                // Begin to create a level
                Level level;
                List<Level> levels = new FilteredElementCollector(_doc).OfClass(typeof(Level)).Cast<Level>().ToList();


                //Obtain the View Template Architectural - FloorPlan
                ViewFamilyType structuralvft = new FilteredElementCollector(_doc).OfClass(typeof(ViewFamilyType))
                    .Cast<ViewFamilyType>()
                    .FirstOrDefault<ViewFamilyType>(x => ViewFamily.FloorPlan == x.ViewFamily);

                ViewPlan vplan;

                using (var transBuildLevels = new Transaction(_doc, "Build Levels"))
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

                    transBuildLevels.Commit();
                }



                // Get a floor type for floor creation
                FilteredElementCollector collector = new FilteredElementCollector(_doc);
                collector.OfClass(typeof(FloorType));

                FloorType floorType = collector.FirstElement() as FloorType;

                //FloorType floorType = new FilteredElementCollector(_doc)
                //    .OfClass(typeof(FloorType))
                //    .First<Element>(
                //      e => e.Name.Equals("Generic - 12\""))
                //      as FloorType;


                // Build a floor profile for the floor creation
                XYZ first = new XYZ(0, 0, elevation);
                XYZ second = new XYZ(FloorOverallLength, 0, elevation);
                XYZ third = new XYZ(FloorOverallLength, FloorOverallWidth, elevation);
                XYZ fourth = new XYZ(0, FloorOverallWidth, elevation);

                CurveArray profile = new CurveArray();

                profile.Append(Line.CreateBound(first, second));
                profile.Append(Line.CreateBound(second, third));
                profile.Append(Line.CreateBound(third, fourth));
                profile.Append(Line.CreateBound(fourth, first));

                // The normal vector (0,0,1) that must be perpendicular to the profile.
                XYZ normal = XYZ.BasisZ;

                using (var transCreateFloorView = new Transaction(_doc, "Create Floor View"))
                {
                    transCreateFloorView.Start();

                    Floor newFloor = _doc.Create.NewFloor(profile, floorType, level, true, normal);

                    transCreateFloorView.Commit();

                }



                WallType wType = new FilteredElementCollector(_doc).OfClass(typeof(WallType))
                                    .Cast<WallType>().FirstOrDefault();


                using (var transCreatewalls = new Transaction(_doc, "Create Walls"))
                {
                    transCreatewalls.Start();

                    Line geomLine1 = Line.CreateBound(first, second);
                    Line geomLine2 = Line.CreateBound(second, third);
                    Line geomLine3 = Line.CreateBound(third, fourth);
                    Line geomLine4 = Line.CreateBound(fourth, first);

                    // Create a wall using the location line
                    var wall = Wall.Create(_doc, geomLine1, level.Id, true);
                    Wall.Create(_doc, geomLine2, level.Id, true);
                    Wall.Create(_doc, geomLine3, level.Id, true);
                    Wall.Create(_doc, geomLine4, level.Id, true);

                    wall.WallType = wType;

                    transCreatewalls.Commit();
                }

                using (var createRegion = new Transaction(_doc, "Create Region"))
                {
                    createRegion.Start();

                    FilteredElementCollector fillRegionTypes
                      = new FilteredElementCollector(_doc)
                        .OfClass(typeof(FilledRegionType));

                    IEnumerable<FilledRegionType> myPatterns =
                        from pattern in fillRegionTypes.Cast<FilledRegionType>()
                        where pattern.Name.Equals("Diagonal Crosshatch")
                        select pattern;

                    foreach (FilledRegionType frt in fillRegionTypes)
                    {
                        List<CurveLoop> profileloops
                          = new List<CurveLoop>();

                        //XYZ[] points = new XYZ[5];
                        //points[0] = new XYZ(0.0, 0.0, 0.0);
                        //points[1] = new XYZ(10.0, 0.0, 0.0);
                        //points[2] = new XYZ(10.0, 10.0, 0.0);
                        //points[3] = new XYZ(0.0, 10.0, 0.0);
                        //points[4] = new XYZ(0.0, 0.0, 0.0);

                        CurveLoop profileloop = new CurveLoop();

                        //for (int i = 0; i < 4; i++)
                        //{
                        //    Line line = Line.CreateBound(points[i], points[i + 1]);

                        //    profileloop.Append(line);
                        //}
                        //profileloops.Add(profileloop);

                        Line geomLine1 = Line.CreateBound(first, second);
                        Line geomLine2 = Line.CreateBound(second, third);
                        Line geomLine3 = Line.CreateBound(third, fourth);
                        Line geomLine4 = Line.CreateBound(fourth, first);

                        profileloop.Append(geomLine1);
                        profileloop.Append(geomLine2);
                        profileloop.Append(geomLine3);
                        profileloop.Append(geomLine4);

                        profileloops.Add(profileloop);


                        ElementId activeViewId = _doc.ActiveView.Id;
                        
                        FilledRegion filledRegion = FilledRegion.Create(_doc, frt.Id, vplan.Id, profileloops);

                        break;
                    }

                    createRegion.Commit();
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

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
