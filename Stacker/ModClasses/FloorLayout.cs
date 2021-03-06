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
        /// Outer boundary of the floor points.
        /// </summary>
        public List<XYPosition> OverallFloorPoints
        {
            get
            {
                return _overallFloorPoints;
            }
        }


        List<List<XYPosition>> _internalHallwayPoints;

        public List<List<XYPosition>> InternalHallwayPoints
        {
            get
            {
                return _internalHallwayPoints;
            }
        }



        private Dictionary<int, XYPosition> _modBlockBasePt;
        public Dictionary<int, XYPosition> ModBlockBasePt
        {
            get
            {
                return _modBlockBasePt;
            }
        }





        public int TotalModBlocks;
        //public Dictionary<int, XYPosition> ModBlockBasePt = new Dictionary<int, XYPosition>();
        public Dictionary<int, double> ModBlockWidth = new Dictionary<int, double>();
        public Dictionary<int, double> ModBlockLength = new Dictionary<int, double>();

        public List<FloorLayoutOption> FloorLayoutOptions = new List<FloorLayoutOption>();





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
            XYPosition topLeft = new XYPosition(0, _overallFloorWidth);
            XYPosition topRight = new XYPosition(_overallFloorLength, _overallFloorWidth);
            XYPosition bottomRight = new XYPosition(_overallFloorLength, 0);

            _overallFloorPoints.Add(origin);
            _overallFloorPoints.Add(topLeft);
            _overallFloorPoints.Add(topRight);
            _overallFloorPoints.Add(bottomRight);

            _modBlockBasePt = new Dictionary<int, XYPosition>();

            _idealModLength = determineModLayoutStacking();

            _internalHallwayPoints = DetermineHallwayPosition();

        }


        public List<List<XYPosition>> DetermineHallwayPosition()
        {
            var internalHallwayPoints = new List<List<XYPosition>>();


            if (FloorModStackScheme == ModStackType.Single)
            {
                TotalModBlocks = 1;

                //Hallway Wall 2
                var wallEnds2 = new List<XYPosition>();

                XYPosition topPoint1 = new XYPosition(0, HallwayWidth);
                XYPosition topPoint2 = new XYPosition(OverallFloorLength, HallwayWidth);

                wallEnds2.Add(topPoint1);
                wallEnds2.Add(topPoint2);

                internalHallwayPoints.Add(wallEnds2);

                _modBlockBasePt[1] = topPoint1;
                ModBlockLength[1] = OverallFloorLength;
                ModBlockWidth[1] = OverallFloorWidth - HallwayWidth;

            }
            else if (FloorModStackScheme == ModStackType.Double)
            {
                TotalModBlocks = 2;

                //Hallway Wall 1
                var wallEnds1 = new List<XYPosition>();

                XYPosition bottomPoint1 = new XYPosition(0, (OverallFloorWidth / 2) + (HallwayWidth / 2));
                XYPosition bottomPoint2 = new XYPosition(OverallFloorLength, (OverallFloorWidth / 2) + (HallwayWidth / 2));

                wallEnds1.Add(bottomPoint1);
                wallEnds1.Add(bottomPoint2);

                internalHallwayPoints.Add(wallEnds1);

                //Hallway Wall 2
                var wallEnds2 = new List<XYPosition>();

                XYPosition topPoint1 = new XYPosition(0, (OverallFloorWidth / 2) - (HallwayWidth / 2));
                XYPosition topPoint2 = new XYPosition(_overallFloorLength, (OverallFloorWidth / 2) - (HallwayWidth / 2));

                wallEnds2.Add(topPoint1);
                wallEnds2.Add(topPoint2);

                internalHallwayPoints.Add(wallEnds2);


                _modBlockBasePt[1] = new XYPosition(0,0);
                _modBlockBasePt[2] = bottomPoint1;

                ModBlockLength[1] = OverallFloorLength;
                ModBlockWidth[1] = (OverallFloorWidth - HallwayWidth) / 2;

                ModBlockLength[2] = OverallFloorLength;
                ModBlockWidth[2] = (OverallFloorWidth - HallwayWidth) / 2;

            }

            return internalHallwayPoints;
        }




        /// <summary>
        /// Determine mod stacking type (single or double) and ideal mod length. 
        /// </summary>
        /// <returns></returns>
        private double determineModLayoutStacking()
        {
            double idealLength = 0;

            //Assuming width is always smaller - for now. And hallway goes parallel to length
            double allowableModLength = OverallFloorWidth - HallwayWidth;

            //Determine stacking type 
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

            //Determine ideal mod length
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
