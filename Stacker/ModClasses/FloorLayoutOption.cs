using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stacker.ModClasses.FloorLayout;

namespace Stacker.ModClasses
{
    class FloorLayoutOption
    {
        public List<FloorModBlock> ModBlock;

        public double ActualTotalFloorWidth;

        public double ActualTotalFloorLength;

        public List<List<XYPosition>> InternalHallwayPoints;

        public XYPosition4Corners FloorOverallExtents;


        public FloorLayoutOption()
        {
            ModBlock = new List<FloorModBlock>();

        }



        public void AddModBlock(FloorModBlock block)
        {
            
            ModBlock.Add(block);

            double bottomLeftX = 0;
            double bottomLeftY = 0;

            double topRightX = 0;
            double topRightY = 0;


            int count = 0;

            foreach (var blk in ModBlock)
            {
                for (var i = 0; i < blk.PlacedMods.Count; i++)
                {
                    XYPosition4Corners test = blk.PlacedModsOuterPosition[i];

                    XYPosition bottomLeft = test.BottomLeft;
                    XYPosition topRight = test.TopRight;

                    if(count == 0)
                    {

                        bottomLeftX = bottomLeft.X;
                        bottomLeftY = bottomLeft.Y;

                        topRightX = topRight.X;
                        topRightY = topRight.Y;
                    }


                    if (bottomLeft.X < bottomLeftX)
                        bottomLeftX = bottomLeft.X;

                    if (bottomLeft.Y < bottomLeftY)
                        bottomLeftY = bottomLeft.Y;


                    if (topRight.X > topRightX)
                        topRightX = topRight.X;

                    if (topRight.Y > topRightY)
                        topRightY = topRight.Y;

                    count++;
                }
            }


            FloorOverallExtents = new XYPosition4Corners();

            XYPosition bottomLeftFinal = new XYPosition(0, 0);
            XYPosition topLeftFinal = new XYPosition(bottomLeftX, topRightY);
            XYPosition topRightFinal = new XYPosition(topRightX, topRightY);
            XYPosition bottomRightFinal = new XYPosition(topRightX, 0);

            FloorOverallExtents.Add4Points(bottomLeftFinal, topLeftFinal, topRightFinal, bottomRightFinal);

            ActualTotalFloorWidth = topLeftFinal.Y - bottomLeftFinal.Y;
            ActualTotalFloorLength = bottomRightFinal.X - bottomLeftFinal.X;

            InternalHallwayPoints = determineModifiedHallwayPosition(block.FloorLayoutReference);
        }



        private List<List<XYPosition>> determineModifiedHallwayPosition(FloorLayout floorLayout)
        {
            List<List<XYPosition>> internalHallwayPoints = new List<List<XYPosition>>();
            
            FloorLayout refFloorLayout = floorLayout;

            if (refFloorLayout.FloorModStackScheme == ModStackType.Single)
            {
                refFloorLayout.TotalModBlocks = 1;

                //Hallway Wall 2
                var wallEnds2 = new List<XYPosition>();

                XYPosition topPoint1 = new XYPosition(0, refFloorLayout.HallwayWidth);
                XYPosition topPoint2 = new XYPosition(ActualTotalFloorLength, refFloorLayout.HallwayWidth);

                wallEnds2.Add(topPoint1);
                wallEnds2.Add(topPoint2);

                internalHallwayPoints.Add(wallEnds2);

            }
            else if (refFloorLayout.FloorModStackScheme == ModStackType.Double)
            {
                refFloorLayout.TotalModBlocks = 2;

                //Hallway Wall 1
                var wallEnds1 = new List<XYPosition>();

                XYPosition bottomPoint1 = new XYPosition(0, (refFloorLayout.OverallFloorWidth / 2) + (refFloorLayout.HallwayWidth / 2));
                XYPosition bottomPoint2 = new XYPosition(ActualTotalFloorLength, (refFloorLayout.OverallFloorWidth / 2) + (refFloorLayout.HallwayWidth / 2));

                wallEnds1.Add(bottomPoint1);
                wallEnds1.Add(bottomPoint2);

                internalHallwayPoints.Add(wallEnds1);

                //Hallway Wall 2
                var wallEnds2 = new List<XYPosition>();

                XYPosition topPoint1 = new XYPosition(0, (refFloorLayout.OverallFloorWidth / 2) - (refFloorLayout.HallwayWidth / 2));
                XYPosition topPoint2 = new XYPosition(ActualTotalFloorLength, (refFloorLayout.OverallFloorWidth / 2) - (refFloorLayout.HallwayWidth / 2));

                wallEnds2.Add(topPoint1);
                wallEnds2.Add(topPoint2);

                internalHallwayPoints.Add(wallEnds2);

            }

            return internalHallwayPoints;
        }




    }
}
