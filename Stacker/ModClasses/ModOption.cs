using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacker.ModClasses
{
    class ModOption
    {
        private ModBase _modBase;
        /// <summary>
        /// ModBase type used in this Mod Option
        /// </summary>
        public ModBase ModBase
        {
            get
            {
                return _modBase;
            }
        }


        private string _modOptionName;
        /// <summary>
        /// Name of this Mod Option
        /// </summary>
        public string ModOptionName
        {
            get
            {
                return _modOptionName;
            }
        }



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


        private double _unitModHeight;
        /// <summary>
        /// Indivdual Module Height
        /// </summary>
        public double UnitModHeight
        {
            get
            {
                return _unitModHeight;
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
        /// Total Area of the selected module (ft sq.)
        /// </summary>
        public double OverallModArea
        {
            get
            {
                return (UnitModWidth * TotalMods) * (UnitModLength);
            }
        }



        private ModGeometry _geometry;
        public ModGeometry Geometry
        {
            get
            {
                return _geometry;
            }
        }



        /// <summary>
        /// Initialize Modoption by setting custom dimensions.
        /// </summary>
        /// <param name="unitModWidth"></param>
        /// <param name="unitModLength"></param>
        public ModOption(double unitModWidth, double unitModLength, ModBase modbase)
        {
            _modBase = modbase;
            _unitModWidth = unitModWidth;
            _unitModLength = unitModLength;

            _modOptionName = $"{modbase.ModName}-{unitModWidth}Wx{unitModLength}L";
            _totalMods = modbase.TotalMods;
            _unitModHeight = modbase.UnitHeight;

            _geometry = new ModGeometry(unitModWidth, unitModLength, _totalMods);


        }




    }
}
