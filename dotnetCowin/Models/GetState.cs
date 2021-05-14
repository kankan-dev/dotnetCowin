using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dotnetCowin.Models
{
    public partial class GetState
    {
        [JsonProperty("states")]
        public List<State> States { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }

    }
    public partial class State
    {
        [JsonProperty("state_id")]
        public long StateId { get; set; }

        [JsonProperty("state_name")]
        public string StateName { get; set; }
    }
}
