using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Structure;
using Stacker.Commands;

namespace Stacker
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CreatePrelimLayout : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document _doc = uidoc.Document;


            try
            {

                CreatePrelimLayoutForm layoutForm = new CreatePrelimLayoutForm(_doc);
                layoutForm.ShowDialog();

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
