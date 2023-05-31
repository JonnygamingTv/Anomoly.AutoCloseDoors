using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.AutoCloseDoors.Models
{
    [JsonObject]
    public class AutoDoorPlayer
    {
        [JsonProperty("steamId")]
        public string SteamId { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}
