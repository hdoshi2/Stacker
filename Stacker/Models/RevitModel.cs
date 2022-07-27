using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stacker.Models
{
    internal class RevitModel
    {
        [JsonProperty(PropertyName = "FloorDetails", Order = 2)]
        public List<FloorData> FloorDetails { get; set; } = new List<FloorData>();

        [JsonProperty(PropertyName = "RevitModelName", Order = 1)]
        public string Name { get; set; }
    }
}
