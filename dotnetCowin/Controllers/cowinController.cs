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
        [Route("GetSessionsByDistrict")]
        public async Task<IActionResult> AvailableSlots()
        {

            try
            {
                HttpClient client = new HttpClient();
                DateTime dt = DateTime.Now;
                string Date = dt.ToString("dd'-'MM'-'yyyy");

                string getState = "";

                string getDistrict = "";

                




                int DistrictID = 20;
                string url =
                    "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/findByDistrict?district_id="+DistrictID +"&date="+Date;

                
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SessionsByDistricts>(json);
                    List<TweetDetails> TList = new List<TweetDetails>();
                    foreach (var item in result.Sessions)
                    {
                        if(item.AvailableCapacity > 0 && item.MinAgeLimit == 45)
                        {

                            TList.Add(new TweetDetails()
                            {
                                Name = item.Name,
                                Address = item.Address,
                                Pincode = item.Pincode,
                                Fee = item.Fee,
                                From = item.From,
                                To = item.To

                            });

                            

                        }
                        else
                        {
                            return Ok("No Slots available right now! Try again later");
                        }

                    }

                    return Ok(TList);
                   


                    



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









        [HttpGet]
        [Route("VaccinationNearMe")]
        public async Task<IActionResult> VaccinationNearMe()
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



                    List<EssentialDetails> EList = new List<EssentialDetails>();

                    foreach (var item in result.Centers)
                    {
                        //if (item.Pincode == pin)
                        //{

                        //}
                    }


                    foreach (var item in result.Centers)
                    {
                        EList.Add(new EssentialDetails()
                        {
                            address = item.Address,
                            name = item.Name
                        });
                    }

                    //for(int i = 0;i < result.Centers.Count; i++)
                    //{
                    //    Er[i] = new EssentialDetails();   
                    //    Er[i].name = result.Centers[i].Name;
                    //    Er[i].address = result.Centers[i].Address;
                    //    //Er[i].VaccineFees = new List<VaccineFeeDetails>()
                    //    //{
                    //    //    new VaccineFeeDetails
                    //    //    {
                    //    //        VaccineName = result.Centers[i].VaccineFees[0].Vaccine,
                    //    //        Fees = result.Centers[i].VaccineFees[0].Fee

                    //    //    }
                    //    //};


                    //}
                    return Ok(EList);


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
