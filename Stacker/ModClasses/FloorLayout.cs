using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacker.ModClasses
{
    class FloorLayout
    {
        private double _overallFloorLength;
        /// <summary>
        /// The overall floor length.
        /// </summary>
        public double OverallFloorLength
        {
            get
            {
                return _overallFloorLength;
            }
        }



        private double _overallFloorWidth;
        /// <summary>
        /// The overall floor width.
        /// </summary>
        public double OverallFloorWidth
        {
            get
            {
                return _overallFloorWidth;
            }
        }


        private double _hallwayWidth;
        /// <summary>
        /// The required hallway width
        /// </summary>
        public double HallwayWidth
        {
            get
            {
                return _hallwayWidth;
            }
        }


        private double _idealModLength;
        /// <summary>
        /// The ideal modular unit length.
        /// </summary>
        public double IdealModLength
        {
            get
            {
                return _idealModLength;
            }
        }


        /// <summary>
        /// Type of modular layout for the floor. 
        /// </summary>
        public enum ModStackType
        {
            Double,
            Single,
            TooSmall
        }


        public ModStackType FloorModStackScheme;



        List<XYPosition> _overallFloorPoints;
        /// <summary>
        /// Outer boundary of the flooor points.
        /// </summary>
        public List<XYPosition> OverallFloorPoints
        {
            get
            {
                return _overallFloorPoints;
            }
        }






        public FloorLayout(double overallFloorLength, double overallFloorWidth, double hallwayWidth)
        {
            _overallFloorLength = overallFloorLength;
            _overallFloorWidth = overallFloorWidth;
            _hallwayWidth = hallwayWidth;

            //
            //Create Outer Coordinates of the Floor
            //
            _overallFloorPoints = new List<XYPosition>();

            XYPosition origin = new XYPosition(0, 0);
            XYPosition topLeft = new XYPosition(0, _overallFloorLength);
            XYPosition topRight = new XYPosition(_overallFloorWidth, _overallFloorLength);
            XYPosition bottomRight = new XYPosition(_overallFloorWidth, 0);

            _overallFloorPoints.Add(origin);
            _overallFloorPoints.Add(topLeft);
            _overallFloorPoints.Add(topRight);
            _overallFloorPoints.Add(bottomRight);

            determineModLayoutStacking();
        }




        private double determineModLayoutStacking()
        {
            double idealLength = 0;


            //Assuming width is always smaller - for now. And hallway goes parallel to length
            double allowableModLength = OverallFloorWidth - HallwayWidth;



            if(allowableModLength >= 20 && allowableModLength <= 35)
            {
                FloorModStackScheme = ModStackType.Single;
            }
            else if (allowableModLength > 35 && allowableModLength <= 70)
            {
                FloorModStackScheme = ModStackType.Double;
            }
            else if(allowableModLength < 20)
            {
                FloorModStackScheme = ModStackType.TooSmall;
            }



            if(FloorModStackScheme == ModStackType.Single)
            {
                idealLength = allowableModLength;
            }
            else if(FloorModStackScheme == ModStackType.Double)
            {
                idealLength = allowableModLength / 2;
            }

            return idealLength;
        }


    }
}
