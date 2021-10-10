using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stacker.ModClasses
{
    /// <summary>
    /// Save results of each element extracted from Revit model 
    /// </summary>
    class BldgResult
    {
        public string LevelName { get; set; }
        public string ElementName { get; set; }
        public double Quantity { get; set; }
        public string UnitType { get; set; }
        public string CategoryType { get; set; }
        public string FamilyName { get; set; }


        public BldgResult()
        {
        }


        /// <summary>
        /// Creates a new Building Result type. 
        /// </summary>
        /// <param name="levelName"></param>
        /// <param name="elementName"></param>
        /// <param name="quantity"></param>
        /// <param name="unitType"></param>
        /// <param name="categoryType"></param>
        /// <param name="familyName"></param>
        public void CreateBldgResult(string levelName = null, string elementName = null, double quantity = 0, string unitType = null, string categoryType = null, string familyName = null)
        {
            LevelName = levelName;
            ElementName = elementName;
            Quantity = quantity;
            UnitType = unitType;
            CategoryType = categoryType;
            FamilyName = familyName;
        }

    }
}
