using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnetCowin.Models
{
    public class GetStateCode
    {
        public async Task<long> getState(string statename)
        {
            HttpClient state = new HttpClient();
            long id = 0;
            string stateUrl = "https://cdn-api.co-vin.in/api/v2/admin/location/states";
            HttpResponseMessage response = await state.GetAsync(stateUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetState>(json);//Storing in the Model GetState
                for (int i = 0; i < result.States.Count; i++)
                {
                    if (result.States[i].StateName.ToUpper() == statename.ToUpper())// Searching among states for state id
                    {
                        id = result.States[i].StateId;
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
