using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacker.ModClasses
{
    /// <summary>
    /// 
    /// </summary>
    class XYPosition
    {
        /// <summary>
        /// 0-based position in X direction
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// 0-based position in X direction
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the XY Position class
        /// </summary>
        /// <param name="x">0-based position in X direction</param>
        /// <param name="y">0-based position in Y direction</param>
        public XYPosition(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
