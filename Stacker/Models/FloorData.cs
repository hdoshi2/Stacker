using Newtonsoft.Json;


namespace Stacker.Models
{
    internal class FloorData
    {
        [JsonProperty(PropertyName = "FloorArea", Order = 2)]
        public double Area { get; set; }

        [JsonProperty(PropertyName = "FloorName", Order = 1)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Units", Order = 3)]
        public string Units { get; set; }
    }
}
