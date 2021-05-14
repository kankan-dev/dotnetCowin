using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dotnetCowin.Models
{
    public partial class GetDistrict
    {
        [JsonProperty("districts")]
        public List<District> Districts { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }
    }
    public partial class District
    {
        [JsonProperty("district_id")]
        public long DistrictId { get; set; }

        [JsonProperty("district_name")]
        public string DistrictName { get; set; }
    }

}
