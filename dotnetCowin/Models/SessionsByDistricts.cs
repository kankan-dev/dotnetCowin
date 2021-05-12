using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetCowin.Models
{
    public partial class SessionsByDistricts
    {
        [JsonProperty("sessions")]
        public List<SessionED> Sessions { get; set; }
    }
    public partial class SessionED
    {
        [JsonProperty("center_id")]
        public long CenterId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("state_name")]
        public string StateName { get; set; }

        [JsonProperty("district_name")]
        public string DistrictName { get; set; }

        [JsonProperty("block_name")]
        public string BlockName { get; set; }

        [JsonProperty("pincode")]
        public long Pincode { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("lat")]
        public long Lat { get; set; }

        [JsonProperty("long")]
        public long Long { get; set; }

        [JsonProperty("fee_type")]
        public string FeeType { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("available_capacity")]
        public long AvailableCapacity { get; set; }

        [JsonProperty("fee")]
        
        public string Fee { get; set; }

        [JsonProperty("min_age_limit")]
        public long MinAgeLimit { get; set; }

        [JsonProperty("vaccine")]
        public string Vaccine { get; set; }

        [JsonProperty("slots")]
        public List<string> Slots { get; set; }
    }

}
