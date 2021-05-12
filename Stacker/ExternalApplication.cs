using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Stacker
{
    class ExternalApplication : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab("Mod Labs");
            string path = Assembly.GetExecutingAssembly().Location;

            PushButtonData button = new PushButtonData("LoadDataFile", "Load Data File", path, "Stacker.CreatePrelimLayout");
            
            RibbonPanel panel = application.CreateRibbonPanel("Mod Labs", "Build Model");

            Uri imagePath = new Uri(@"C:\Users\hdoshi\Desktop\Modular\Repos\building_Crane.png");
            BitmapImage image = new BitmapImage(imagePath);


            PushButton pushButton = panel.AddItem(button) as PushButton;
            pushButton.LargeImage = image;


            return Result.Succeeded;
        }
    }
}
