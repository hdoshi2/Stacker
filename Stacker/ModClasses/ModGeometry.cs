using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacker.ModClasses
{
    class ModGeometry
    {

        private double _unitModWidth;
        /// <summary>
        /// Indivdual Module Width
        /// </summary>
        public double UnitModWidth
        {
            get
            {
                return _unitModWidth;
            }
        }




        private double _unitModLength;
        /// <summary>
        /// Indivdual Module Length
        /// </summary>
        public double UnitModLength
        {
            get
            {
                return _unitModLength;
            }
        }




        private double _totalModWidth;
        /// <summary>
        /// Total Module Width
        /// </summary>
        public double TotalModWidth
        {
            get
            {
                return _totalModWidth;
            }
        }




        private double _totalModLength;
        /// <summary>
        /// Total Module Length
        /// </summary>
        public double TotalModLength
        {
            get
            {
                return _totalModLength;
            }
        }


        private double _totalModArea;
        /// <summary>
        /// Total Module Area
        /// </summary>
        public double TotalModArea
        {
            get
            {
                return _totalModArea;
            }
        }




        private int _totalMods;
        /// <summary>
        /// Total Modules
        /// </summary>
        public int TotalMods
        {
            get
            {
                return _totalMods;
            }
        }



        /// <summary>
        /// Overall available length for details in X direction
        /// </summary>
        public int InternalWalls
        {
            get
            {
                return TotalMods - 1;
            }
        }


        List<XYPosition> _outerBlockPoints;

        public List<XYPosition> OuterBlockPoints
        {
            get
            {
                return _outerBlockPoints;
            }
        }


        List<List<XYPosition>> _internalWallPoints;

        public List<List<XYPosition>> InternalWallPoints
        {
            get
            {
                return _internalWallPoints;
            }
        }


        /// <summary>
        /// Initialize a Module Geometry Classification.
        /// </summary>
        /// <param name="unitModWidth"></param>
        /// <param name="unitModLength"></param>
        /// <param name="totalMods"></param>
        public ModGeometry(double unitModWidth, double unitModLength, int totalMods)
        {
            _unitModWidth = unitModWidth;
            _unitModLength = unitModLength;
            _totalMods = totalMods;

            _totalModWidth = _unitModWidth * _totalMods;
            _totalModLength = _unitModLength;
            _totalModArea = _totalModWidth * _totalModLength;

            //
            //Create Outer Coordinates of the Block
            //
            _outerBlockPoints = new List<XYPosition>();

            XYPosition origin = new XYPosition(0, 0);
            XYPosition topLeft = new XYPosition(0, _totalModLength);
            XYPosition topRight = new XYPosition(_totalModWidth, _totalModLength);
            XYPosition bottomRight = new XYPosition(_totalModWidth, 0);

            _outerBlockPoints.Add(origin);
            _outerBlockPoints.Add(topLeft);
            _outerBlockPoints.Add(topRight);
            _outerBlockPoints.Add(bottomRight);


            //
            //Create Internal Wall Dimensions
            //
            _internalWallPoints = new List<List<XYPosition>>();

            for (var i = 1; i < _totalMods; i++)
            {
                var wallEnds= new List<XYPosition>();

                XYPosition bottomPoint = new XYPosition(_unitModWidth * i, 0);
                XYPosition topPoint = new XYPosition(_unitModWidth * i, _unitModLength);

                wallEnds.Add(topPoint);
                wallEnds.Add(bottomPoint);

                _internalWallPoints.Add(wallEnds);
            }


        }





    }
}
