
using dotnetCowin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnetCowin.Controllers
{
    [Route("api/cowin")]
    [ApiController]
    public class cowinController : ControllerBase
    {
        [HttpGet]
        [Route("GetCalendarByPin")]
        public async Task<IActionResult> GetCalendarByPin()
        {
            try
            {
                DateTime dt = DateTime.Now;
                string date = dt.ToString("dd'-'MM'-'yyyy");
                string pin = "560076";
                
                string url = "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/calendarByPin?pincode="+pin+"&date="+date;
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<VaccineNearYou>(json);
                    EssentialDetails[] Er = new EssentialDetails[result.Centers.Count];

                    for(int i = 0;i < result.Centers.Count; i++)
                    {
                        Er[i] = new EssentialDetails();   
                        Er[i].name = result.Centers[i].Name;
                        Er[i].address = result.Centers[i].Address;
                        //Er[i].VaccineFees = new List<VaccineFeeDetails>()
                        //{
                        //    new VaccineFeeDetails
                        //    {
                        //        VaccineName = result.Centers[i].VaccineFees[0].Vaccine,
                        //        Fees = result.Centers[i].VaccineFees[0].Fee

                        //    }
                        //};

                    
                    }
                    return Ok(Er);


                    //{
                    //    name = result.Centers[0].Name,
                    //    address = result.Centers[0].Address,
                    //    VaccineFees  = new List<VaccineFeeDetails>()
                    //    {
                    //        new VaccineFeeDetails
                    //        {
                    //                VaccineName= result.Centers[0].VaccineFees[0].Vaccine,
                    //                Fees = result.Centers[0].VaccineFees[0].Fee
                    //        }

                    //    }



                    //};

                }
                else
                {
                    return BadRequest("Error while retrieving");
                }




            }
            catch(Exception e)
            {
                return NotFound(e.Message + " " + e.StackTrace);
            }
            
        }
    }
}
