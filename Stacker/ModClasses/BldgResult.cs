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

    }
}
