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

            PodLengthMin = 15;
            PodLengthMax = 25;
            PodWidthMin = 10;
            PodWidthMax = 16;
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



                using (var transBuildLevels = new Transaction(_doc, "Build Levels"))
                {
                    transBuildLevels.Start();

                    level = Level.Create(_doc, elevation);

                    if (null == level)
                        throw new Exception("Create a new level failed.");

                    // Change the level name
                    level.Name = "New level";


                    //Create a New View
                    ViewPlan vplan = ViewPlan.Create(_doc, structuralvft.Id, level.Id);
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


                //TEST
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CreatePrelimLayoutForm.ActiveForm.Close();
        }
    }
}
