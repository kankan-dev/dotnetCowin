using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnetCowin.Models
{
    public class GetDistrictCode
    {
        public async Task<long> getDistrict(string districtName, long stateId)
        {
            HttpClient district = new HttpClient();
            long id = 0;
            string districtUrl = "https://cdn-api.co-vin.in/api/v2/admin/location/districts/" + stateId; // stateId is retrived from GetStateCode class.
            HttpResponseMessage response = await district.GetAsync(districtUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetDistrict>(json);

                for (int i = 0; i < result.Districts.Count; i++)
                {
                    if (result.Districts[i].DistrictName.ToUpper() == districtName.ToUpper())// Searching for District Id for the District Name Provided by the user
                    {
                        id = result.Districts[i].DistrictId;
                    }

                }
                return id;
            }
            else
            {
                return 0;
            }
        }
    }
}
