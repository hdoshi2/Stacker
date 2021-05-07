using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

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

                using (var T = new Transaction(_doc, "Build Levels"))
                {
                    T.Start();

                    // The elevation to apply to the new level
                    double elevation = 20.0;

                    // Begin to create a level
                    Level level = Level.Create(_doc, elevation);
                    if (null == level)
                    {
                        throw new Exception("Create a new level failed.");
                    }

                    // Change the level name
                    level.Name = "New level";

                    //Obtain the View Template Architectural - FloorPlan
                    ViewFamilyType structuralvft = new FilteredElementCollector(_doc).OfClass(typeof(ViewFamilyType))
                        .Cast<ViewFamilyType>()
                        .FirstOrDefault<ViewFamilyType>(x => ViewFamily.FloorPlan == x.ViewFamily);


                    //Create a New View
                    ViewPlan vplan = ViewPlan.Create(_doc, structuralvft.Id, level.Id);
                    vplan.Name = level.Name + " - TEST";


                    T.Commit();






                    //FilteredElementCollector levels
                    //  = new FilteredElementCollector(_doc)
                    //    .OfClass(typeof(Level));

                    //FloorType floorType
                    //  = new FilteredElementCollector(_doc)
                    //    .OfClass(typeof(FloorType))
                    //    .First<Element>(
                    //      e => e.Name.Equals("Generic - 12\""))
                    //      as FloorType;

                    //Element profileElement
                    //  = new FilteredElementCollector(_doc)
                    //    .OfClass(typeof(FamilyInstance))
                    //    .OfCategory(BuiltInCategory.OST_GenericModel)
                    //    .First<Element>(
                    //      e => e.Name.Equals("WP1"));

                    //CurveArray slabCurves = new CurveArray();

                    //GeometryElement geo = profileElement.get_Geometry(new Options());

                    //foreach (GeometryInstance inst in geo.Objects)
                    //{
                    //    foreach (GeometryObject obj in inst.SymbolGeometry.Objects)
                    //    {
                    //        if (obj is Curve)
                    //        {
                    //            slabCurves.Append(obj as Curve);
                    //        }
                    //    }
                    //}

                    //XYZ normal = XYZ.BasisZ;

                    //Transaction trans = new Transaction(_doc);
                    //trans.Start("Create Floors");

                    //foreach (Level lvl in levels)
                    //{
                    //    Floor newFloor = _doc.Create.NewFloor(slabCurves, floorType, lvl, false, normal);

                    //    newFloor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM).Set(0);
                    //}

                    //trans.Commit();




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
                    XYZ first = new XYZ(0, 0, 0);
                    XYZ second = new XYZ(20, 0, 0);
                    XYZ third = new XYZ(20, 15, 0);
                    XYZ fourth = new XYZ(0, 15, 0);
                    CurveArray profile = new CurveArray();
                    profile.Append(Line.CreateBound(first, second));
                    profile.Append(Line.CreateBound(second, third));
                    profile.Append(Line.CreateBound(third, fourth));
                    profile.Append(Line.CreateBound(fourth, first));

                    // The normal vector (0,0,1) that must be perpendicular to the profile.
                    XYZ normal = XYZ.BasisZ;

                    Transaction trans = new Transaction(_doc);
                    trans.Start("Create Floors");

                    Floor newFloor = _doc.Create.NewFloor(profile, floorType, level, true, normal);


                    trans.Commit();


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
