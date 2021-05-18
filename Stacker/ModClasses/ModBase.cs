using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacker.ModClasses
{
    class ModBase
    {
        public string ModName { get; set; }

        public int TotalMods { get; set; }



        private double _unitWidthMax;
        /// <summary>
        /// Maximum width for this Module
        /// </summary>
        public double UnitWidthMax
        {
            get
            {
                return _unitWidthMax;
            }
        }



        private double _unitWidthMin;
        /// <summary>
        /// Minimum width for this Module
        /// </summary>
        public double UnitWidthMin
        {
            get
            {
                return _unitWidthMin;
            }
        }



        private double _unitLengthMin;
        /// <summary>
        /// Maximum length for this Module
        /// </summary>
        public double UnitLengthMin
        {
            get
            {
                return _unitLengthMin;
            }
        }




        private double _unitLengthMax;
        /// <summary>
        /// Minimum length for this Module
        /// </summary>
        public double UnitLengthMax
        {
            get
            {
                return _unitLengthMax;
            }
        }




        private double _unitHeight;
        /// <summary>
        /// Height for this Module
        /// </summary>
        public double UnitHeight
        {
            get
            {
                return _unitHeight;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="modName"></param>
        /// <param name="totalMods"></param>
        /// <param name="unitWidthMax"></param>
        /// <param name="unitWidthMin"></param>
        /// <param name="unitLengthMax"></param>
        /// <param name="unitLengthMin"></param>
        /// <param name="unitHeight"></param>
        public ModBase(string modName, int totalMods, double unitWidthMax, double unitWidthMin, double unitLengthMax, double unitLengthMin, double unitHeight)
        {
            string ModName = modName;
            int TotalMods = totalMods;

            double _unitWidthMax = unitWidthMax;
            double _unitWidthMin = unitWidthMin;
            double _unitLengthMax = unitLengthMax;
            double _unitLengthMin = unitLengthMin;
            double _unitHeight = unitHeight;

        }






    }



}
