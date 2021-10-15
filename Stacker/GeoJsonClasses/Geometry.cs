using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stacker.GeoJsonClasses
{
    /// <summary>
    /// Defines the <see cref="Geometry" />.
    /// </summary>
    [JsonConverter(typeof(GeometryConverter))]
    public class Geometry
    {
        /// <summary>
        /// Gets or sets the Coordinates.
        /// </summary>
        public List<List<Polygon>> PolygonsSet { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        public string Type { get; set; }
    }
}
