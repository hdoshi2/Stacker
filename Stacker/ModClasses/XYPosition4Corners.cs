using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacker.ModClasses
{
    class XYPosition4Corners
    {

        /// <summary>
        /// BottomLeft corner
        /// </summary>
        public XYPosition BottomLeft { get; set; }

        /// <summary>
        /// TopLeft corner
        /// </summary>
        public XYPosition TopLeft { get; set; }

        /// <summary>
        /// TopRight corner
        /// </summary>
        public XYPosition TopRight { get; set; }

        /// <summary>
        /// BottomRight corner
        /// </summary>
        public XYPosition BottomRight { get; set; }


        /// <summary>
        /// Initializes a new instance of store 4 corner corner XY Points of a box. 
        /// </summary>
        public XYPosition4Corners()
        {

        }


        public void Add4Points(XYPosition bottomLeft, XYPosition topLeft, XYPosition topRight, XYPosition bottomRight)
        {
            BottomLeft = bottomLeft;
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
        }



        public List<XYPosition> getXYPositions()
        {
            List<XYPosition> newList = new List<XYPosition>();

            newList.Add(BottomLeft);
            newList.Add(TopLeft);
            newList.Add(TopRight);
            newList.Add(BottomRight);

            return newList;
        }

    }
}
