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

        public XYPosition GlobalBasePt;

        public XYPosition NextAvailableBasePt;

        public List<XYPosition> OuterModBlockPts;

        public double TotalBlockWidth;

        public double TotalBlockLength;

        public double SFModTotal;

        public double SFModAvailable;


        public double AvailableBlockLength;



        public int PlacedModCount;

        public Dictionary<int, ModOption> PlacedMods;

        public Dictionary<int, XYPosition> PlacedModsBasePosition;

        public Dictionary<int, List<XYPosition>> PlacedModsOuterPosition;

        public Dictionary<int, double> SFBlock;



        public FloorModBlock(string name, XYPosition globalBasePt, double totalBlockWidth, double totalBlockLength)
        {
            Name = name;
            
            GlobalBasePt = globalBasePt;
            NextAvailableBasePt = globalBasePt;

            TotalBlockWidth = totalBlockWidth;
            TotalBlockLength = totalBlockLength;
            AvailableBlockLength = TotalBlockLength;

            SFModTotal = totalBlockWidth * TotalBlockLength;
            SFModAvailable = totalBlockWidth * TotalBlockLength;

            PlacedModCount = 0;
            PlacedMods = new Dictionary<int, ModOption>();
            PlacedModsBasePosition = new Dictionary<int, XYPosition>();
            PlacedModsOuterPosition = new Dictionary<int, List<XYPosition>>();
            SFBlock = new Dictionary<int, double>();

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



        public bool ValidateBlockAdd(ModOption modToAdd)
        {
            ModGeometry modGeometry = modToAdd.Geometry;

            if (AvailableBlockLength < modGeometry.TotalModWidth)
            {
                return false;
            }

            if (TotalBlockWidth < modGeometry.TotalModLength)
            {
                return false;
            }

            if (SFModTotal < modGeometry.TotalModArea)
            {
                return false;
            }

            return true;
        }



        public int AddBlock(ModOption modToAdd)
        {
            var modGeometry = modToAdd.Geometry;

            if(ValidateBlockAdd(modToAdd) == false)
            {
                return 0;
            }

            PlacedMods[PlacedModCount] = modToAdd;
            PlacedModsBasePosition[PlacedModCount] = NextAvailableBasePt;

            List<XYPosition> modGlobalPts = new List<XYPosition>();

            XYPosition origin = new XYPosition(NextAvailableBasePt.X, NextAvailableBasePt.Y);
            XYPosition topLeft = new XYPosition(NextAvailableBasePt.X, NextAvailableBasePt.Y + modToAdd.UnitModLength);
            XYPosition topRight = new XYPosition(NextAvailableBasePt.X + modToAdd.Geometry.TotalModWidth, NextAvailableBasePt.Y + modToAdd.Geometry.TotalModLength);
            XYPosition bottomRight = new XYPosition(NextAvailableBasePt.X + modToAdd.Geometry.TotalModWidth, NextAvailableBasePt.Y);

            modGlobalPts.Add(origin);
            modGlobalPts.Add(topLeft);
            modGlobalPts.Add(topRight);
            modGlobalPts.Add(bottomRight);

            PlacedModsOuterPosition[PlacedModCount] = modGlobalPts;

            NextAvailableBasePt.X = NextAvailableBasePt.X + modGeometry.TotalModWidth;
            AvailableBlockLength = AvailableBlockLength - modGeometry.TotalModWidth;
            SFModAvailable = SFModAvailable - modGeometry.TotalModArea;

            PlacedModCount++;

            return 1;
        }



    }
}
