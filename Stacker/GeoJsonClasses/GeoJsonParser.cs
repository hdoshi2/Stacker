using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

namespace Stacker.GeoJsonClasses
{
    class GeoJsonParser
    {
        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="GeoJsonResultCollection"/>.</returns>
        public GeoJsonResultCollection ParseFile(string fileName)
        {
            try
            {
                using (var file = File.OpenText(fileName))
                {
                    var serializer = new JsonSerializer();

                    return (GeoJsonResultCollection)serializer.Deserialize(file, typeof(GeoJsonResultCollection));
                }
            }
            catch (Exception e)
            {
                TaskDialog.Show("Error!", "Error converting GeoJson file to Revit\n" + e.Message);
            }

            return null;
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="GeoJsonResultCollection"/>.</returns>
        public GeoJsonResultCollection ParseJSON(string json)
        {
            try
            {
                var serializer = new JsonSerializer();
                TextReader textReader = new StringReader(json);
                return (GeoJsonResultCollection)serializer.Deserialize(textReader, typeof(GeoJsonResultCollection));

            }
            catch (Exception e)
            {
                TaskDialog.Show("Error!", "Error converting GeoJson file to Revit\n" + e.Message);
            }

            return null;
        }


    }
}
