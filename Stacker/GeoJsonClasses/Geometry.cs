using System.Collections.Generic;

namespace Stacker.GeoJsonClasses
{
    /// <summary>
    /// Defines the <see cref="Geometry" />.
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// Gets or sets the Coordinates.
        /// </summary>
        public List<List<List<double>>> Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        public string Type { get; set; }
    }
}
