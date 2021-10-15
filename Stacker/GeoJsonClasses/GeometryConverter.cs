using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Stacker.GeoJsonClasses
{
    public class GeometryConverter : JsonConverter
    {
        /// <summary>
        /// The CanConvert.
        /// </summary>
        /// <param name="objectType">The objectType<see cref="Type"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        /// <summary>
        /// The ReadJson.
        /// </summary>
        /// <param name="reader">The reader<see cref="JsonReader"/>.</param>
        /// <param name="objectType">The objectType<see cref="Type"/>.</param>
        /// <param name="existingValue">The existingValue<see cref="object"/>.</param>
        /// <param name="serializer">The serializer<see cref="JsonSerializer"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var polygonsSet = new List<List<Polygon>>();
            var jsonData = (JObject)serializer.Deserialize(reader);
            var coordinatesData = (JArray)jsonData["coordinates"];

            if (jsonData["type"].ToString().Equals("Polygon", StringComparison.OrdinalIgnoreCase))
            {
                var polygons = new List<Polygon>();

                foreach (var p in coordinatesData)
                {
                    var polygon = new Polygon
                    {
                        Coordinates = p.Select(a => new Coordinate()
                        {
                            longitude = Convert.ToDouble(a[0]),
                            latitude = Convert.ToDouble(a[1])
                        }).ToList()
                    };

                    polygons.Add(polygon);
                }

                return new Geometry() { Type = "Polygon", PolygonsSet = new List<List<Polygon>>() { polygons } };
            }
            else if (jsonData["type"].ToString().Equals("MultiPolygon", StringComparison.OrdinalIgnoreCase))
            {
                var polygons = new List<List<Polygon>>();

                foreach (var ps in coordinatesData)
                {
                    var subPolygons = new List<Polygon>();

                    foreach (var p in ps)
                    {
                        var polygon = new Polygon
                        {
                            Coordinates = p.Select(a => new Coordinate()
                            {
                                longitude = Convert.ToDouble(a[0]),
                                latitude = Convert.ToDouble(a[1])
                            }).ToList()
                        };

                        subPolygons.Add(polygon);
                    }

                    polygons.Add(subPolygons);
                }

                return new Geometry() { Type = "MultiPolygon", PolygonsSet = polygons };
            }

            return null;
        }

        /// <summary>
        /// The WriteJson.
        /// </summary>
        /// <param name="writer">The writer<see cref="JsonWriter"/>.</param>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="serializer">The serializer<see cref="JsonSerializer"/>.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
