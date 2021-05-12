using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Structure;

namespace Stacker
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class GetElementId : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document _doc = uidoc.Document;


            try
            {

                // The elevation to apply to the new level
                double elevation = 20.0;

                // Begin to create a level
                Level level = Level.Create(_doc, elevation);
                FilteredElementCollector levels = new FilteredElementCollector(_doc).OfClass(typeof(Level));

                if (null == level)
                    throw new Exception("Create a new level failed.");

                // Change the level name
                level.Name = "New level";

                //Obtain the View Template Architectural - FloorPlan
                ViewFamilyType structuralvft = new FilteredElementCollector(_doc).OfClass(typeof(ViewFamilyType))
                    .Cast<ViewFamilyType>()
                    .FirstOrDefault<ViewFamilyType>(x => ViewFamily.FloorPlan == x.ViewFamily);



                using (var transBuildLevels = new Transaction(_doc, "Build Levels"))
                {
                    transBuildLevels.Start();

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
                XYZ second = new XYZ(200, 0, elevation);
                XYZ third = new XYZ(200, 150, elevation);
                XYZ fourth = new XYZ(0, 150, elevation);

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





                return Result.Succeeded;
            }
            catch(Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }


        }

    }
}
