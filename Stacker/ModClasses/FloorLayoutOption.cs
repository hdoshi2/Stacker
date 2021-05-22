using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            XYPosition bottomLeftFinal = new XYPosition(0, bottomLeftY);
            XYPosition topLeftFinal = new XYPosition(bottomLeftX, topRightY);
            XYPosition topRightFinal = new XYPosition(topRightX, topRightY);
            XYPosition bottomRightFinal = new XYPosition(topRightX, 0);

            FloorOverallExtents.Add4Points(bottomLeftFinal, topLeftFinal, topRightFinal, bottomRightFinal);


        }

    }
}
