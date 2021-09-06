using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacker.ModClasses
{
    class FloorModBlock
    {

        public string Name { get; set; }

        public FloorLayout FloorLayoutReference { get; set; }

        private XYPosition GlobalBasePt { get; set; }

        public XYPosition NextAvailableBasePt { get; set; }

        public XYPosition4Corners OuterModBlockPts { get; set; }

        public double TotalBlockWidth { get; set; }

        public double TotalBlockLength { get; set; }

        public double SFModTotal { get; set; }

        public double SFModAvailable { get; set; }

        public double SFModFilled { get; set; }

        public double BlockLengthAvailable { get; set; }

        public double BlockLengthUsed { get; set; }

        public int PlacedModCount { get; set; }


        public Dictionary<int, ModOption> PlacedMods { get; set; }

        public Dictionary<int, XYPosition> PlacedModsBasePosition { get; set; }

        public Dictionary<int, XYPosition4Corners> PlacedModsOuterPosition { get; set; }

        public Dictionary<int, double> SFBlock { get; set; }




        /// <summary>
        /// Constructor for a block on the floor containing numerous mods (units)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="globalBasePt"></param>
        /// <param name="totalBlockWidth"></param>
        /// <param name="totalBlockLength"></param>
        /// <param name="floorLayout"></param>
        public FloorModBlock(string name, XYPosition globalBasePt, double totalBlockWidth, double totalBlockLength, FloorLayout floorLayout)
        {
            Name = name;
            FloorLayoutReference = floorLayout;

            GlobalBasePt = new XYPosition(globalBasePt.X, globalBasePt.Y);
            NextAvailableBasePt = globalBasePt;

            TotalBlockWidth = totalBlockWidth;
            TotalBlockLength = totalBlockLength;
            BlockLengthAvailable = TotalBlockLength;
            BlockLengthUsed = 0;

            SFModTotal = totalBlockWidth * TotalBlockLength;
            SFModAvailable = totalBlockWidth * TotalBlockLength;
            SFModFilled = SFModTotal - SFModAvailable;

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
            SFModFilled = SFModTotal - SFModAvailable;

            PlacedModCount++;

            return 1;
        }





        public Dictionary<int, ModOption> PlacedMods_Modified { get; set; }

        public Dictionary<int, XYPosition4Corners> PlacedModsOuterPosition_Modified { get; set; }

        public XYPosition NextAvailableBasePt_Modified { get; set; }



        public void ModifyModPositions()
        {
            PlacedMods_Modified = new Dictionary<int, ModOption>();
            PlacedModsOuterPosition_Modified = new Dictionary<int, XYPosition4Corners>();
            NextAvailableBasePt_Modified = GlobalBasePt;

            List<ModOption> newModList = PlacedMods.Values.ToList();

            Random rng = new Random();
            List<ModOption> shuffledMods = newModList.OrderBy(a => rng.Next()).ToList();

            int placedModCount = 0;

            foreach (var modToAdd in shuffledMods)
            {
                var modGeometry = modToAdd.Geometry;

                PlacedMods_Modified[placedModCount] = modToAdd;

                XYPosition4Corners modGlobalPoints = new XYPosition4Corners();

                XYPosition origin = new XYPosition(NextAvailableBasePt_Modified.X, NextAvailableBasePt_Modified.Y);
                XYPosition topLeft = new XYPosition(NextAvailableBasePt_Modified.X, NextAvailableBasePt_Modified.Y + modToAdd.UnitModLength);
                XYPosition topRight = new XYPosition(NextAvailableBasePt_Modified.X + modToAdd.Geometry.TotalModWidth, NextAvailableBasePt_Modified.Y + modToAdd.Geometry.TotalModLength);
                XYPosition bottomRight = new XYPosition(NextAvailableBasePt_Modified.X + modToAdd.Geometry.TotalModWidth, NextAvailableBasePt_Modified.Y);

                modGlobalPoints.Add4Points(origin, topLeft, topRight, bottomRight);

                PlacedModsOuterPosition_Modified[placedModCount] = modGlobalPoints;

                NextAvailableBasePt_Modified.X = NextAvailableBasePt_Modified.X + modGeometry.TotalModWidth;

                placedModCount++;

            }

            PlacedMods = PlacedMods_Modified;
            PlacedModsOuterPosition = PlacedModsOuterPosition_Modified;

        }



    }
}
