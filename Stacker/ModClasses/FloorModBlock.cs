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

        public FloorLayout FloorLayoutReference;

        public XYPosition GlobalBasePt;

        public XYPosition NextAvailableBasePt;

        public XYPosition4Corners OuterModBlockPts;

        public double TotalBlockWidth;

        public double TotalBlockLength;

        public double SFModTotal;

        public double SFModAvailable;

        public double BlockLengthAvailable;

        public double BlockLengthUsed;


        public int PlacedModCount;

        public Dictionary<int, ModOption> PlacedMods;

        public Dictionary<int, XYPosition> PlacedModsBasePosition;

        public Dictionary<int, XYPosition4Corners> PlacedModsOuterPosition;

        public Dictionary<int, double> SFBlock;



        public FloorModBlock(string name, XYPosition globalBasePt, double totalBlockWidth, double totalBlockLength, FloorLayout floorLayout)
        {
            Name = name;
            FloorLayoutReference = floorLayout;

            GlobalBasePt = globalBasePt;
            NextAvailableBasePt = globalBasePt;

            TotalBlockWidth = totalBlockWidth;
            TotalBlockLength = totalBlockLength;
            BlockLengthAvailable = TotalBlockLength;
            BlockLengthUsed = 0;

            SFModTotal = totalBlockWidth * TotalBlockLength;
            SFModAvailable = totalBlockWidth * TotalBlockLength;

            PlacedModCount = 0;
            PlacedMods = new Dictionary<int, ModOption>();
            PlacedModsBasePosition = new Dictionary<int, XYPosition>();
            PlacedModsOuterPosition = new Dictionary<int, XYPosition4Corners>();
            SFBlock = new Dictionary<int, double>();

            //
            //Create Outer Coordinates of the Block
            //
            OuterModBlockPts = new XYPosition4Corners();

            XYPosition origin = GlobalBasePt;
            XYPosition topLeft = new XYPosition(GlobalBasePt.X, GlobalBasePt.Y + TotalBlockWidth);
            XYPosition topRight = new XYPosition(GlobalBasePt.X + TotalBlockLength, GlobalBasePt.Y + TotalBlockWidth);
            XYPosition bottomRight = new XYPosition(GlobalBasePt.X + TotalBlockLength, GlobalBasePt.Y);

            OuterModBlockPts.Add4Points(origin, topLeft, topRight, bottomRight);

        }



        public bool ValidateBlockAdd(ModOption modToAdd)
        {
            ModGeometry modGeometry = modToAdd.Geometry;

            if (BlockLengthAvailable < modGeometry.TotalModWidth)
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



        public int AddModToBlock(ModOption modToAdd)
        {
            var modGeometry = modToAdd.Geometry;

            if(ValidateBlockAdd(modToAdd) == false)
            {
                return 0;
            }

            PlacedMods[PlacedModCount] = modToAdd;
            PlacedModsBasePosition[PlacedModCount] = NextAvailableBasePt;

            XYPosition4Corners modGlobalPoints = new XYPosition4Corners();

            XYPosition origin = new XYPosition(NextAvailableBasePt.X, NextAvailableBasePt.Y);
            XYPosition topLeft = new XYPosition(NextAvailableBasePt.X, NextAvailableBasePt.Y + modToAdd.UnitModLength);
            XYPosition topRight = new XYPosition(NextAvailableBasePt.X + modToAdd.Geometry.TotalModWidth, NextAvailableBasePt.Y + modToAdd.Geometry.TotalModLength);
            XYPosition bottomRight = new XYPosition(NextAvailableBasePt.X + modToAdd.Geometry.TotalModWidth, NextAvailableBasePt.Y);

            modGlobalPoints.Add4Points(origin, topLeft, topRight, bottomRight);

            PlacedModsOuterPosition[PlacedModCount] = modGlobalPoints;

            NextAvailableBasePt.X = NextAvailableBasePt.X + modGeometry.TotalModWidth;

            BlockLengthAvailable = BlockLengthAvailable - modGeometry.TotalModWidth;
            BlockLengthUsed = BlockLengthUsed + modToAdd.Geometry.TotalModWidth;

            SFModAvailable = SFModAvailable - modGeometry.TotalModArea;

            PlacedModCount++;

            return 1;
        }



    }
}
