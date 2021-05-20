using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacker.ModClasses
{
    class FloorModBlock
    {
        public string Name;

        XYPosition GlobalBasePt;

        XYPosition NextAvailableBasePt;

        List<XYPosition> OuterModBlockPts;

        double TotalBlockWidth;

        double TotalBlockLength;

        double SFTotal;

        double SFAvailable;

        double AvailableBlockLength;



        int PlacedModCount;

        Dictionary<int, ModOption> PlacedMods;

        Dictionary<int, XYPosition> PlacedModsBasePosition;



        public FloorModBlock(string name, XYPosition globalBasePt, double totalBlockWidth, double totalBlockLength)
        {
            Name = name;
            
            GlobalBasePt = globalBasePt;
            NextAvailableBasePt = globalBasePt;

            TotalBlockWidth = totalBlockWidth;
            TotalBlockLength = totalBlockLength;
            AvailableBlockLength = TotalBlockLength;

            SFTotal = totalBlockWidth * TotalBlockLength;
            SFAvailable = totalBlockWidth * TotalBlockLength;

            PlacedModCount = 0;
            PlacedMods = new Dictionary<int, ModOption>();
            PlacedModsBasePosition = new Dictionary<int, XYPosition>();


            //
            //Create Outer Coordinates of the Block
            //
            OuterModBlockPts = new List<XYPosition>();

            XYPosition origin = GlobalBasePt;
            XYPosition topLeft = new XYPosition(GlobalBasePt.X, GlobalBasePt.Y + TotalBlockWidth);
            XYPosition topRight = new XYPosition(GlobalBasePt.X + TotalBlockLength, GlobalBasePt.Y + TotalBlockWidth);
            XYPosition bottomRight = new XYPosition(GlobalBasePt.X + TotalBlockLength, GlobalBasePt.Y);

            OuterModBlockPts.Add(origin);
            OuterModBlockPts.Add(topLeft);
            OuterModBlockPts.Add(topRight);
            OuterModBlockPts.Add(bottomRight);

        }




        public int AddBlock(ModOption modToAdd)
        {
            var modGeometry = modToAdd.Geometry;

            if(AvailableBlockLength < modGeometry.TotalModWidth)
            {
                return 0;
            }

            if(TotalBlockWidth < modGeometry.TotalModLength)
            {
                return 0;
            }

            if (SFTotal < modGeometry.TotalModArea)
            {
                return 0;
            }

            PlacedMods[PlacedModCount] = modToAdd;
            PlacedModsBasePosition[PlacedModCount] = NextAvailableBasePt;

            NextAvailableBasePt.X = modGeometry.TotalModWidth;
            AvailableBlockLength = AvailableBlockLength - modGeometry.TotalModLength;
            SFAvailable = SFAvailable - modGeometry.TotalModArea;

            PlacedModCount++;

            return 1;
        }



    }
}
