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
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;


namespace dotnetCowin.Controllers
{
    [Route("api/cowin")]
    [ApiController]
    public class cowinController : ControllerBase
    {
        [HttpPost]
        [Route("GetSessionsByDistrict")]
        public async Task<IActionResult> AvailableSlots([FromBody] EnterDetails enter)
        {
            GetStateCode stateCode = new GetStateCode();
            var stateId = await stateCode.getState(enter.state);

            GetDistrictCode districtCode = new GetDistrictCode();
            long districtID = districtCode.getDistrict(enter.district, stateId).Result;

            try
            {
                HttpClient client = new HttpClient();
                DateTime dt = DateTime.Now;
                string Date = dt.ToString("dd'-'MM'-'yyyy");                
                string url =
                "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/findByDistrict?district_id="+districtID+"&date="+Date;

                string AccountSid = "ACf082c56dca8a2201e8724718e6d8c36d";
                string AccountAuth = "8f3a5d356e84e96767987e51ab60da79";
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
                                To = item.To,
                                AvailableCapacity = item.AvailableCapacity,
                                FirstDose = item.Dose1,
                                SecondDose = item.Dose2

                            });

                            

                        }
                        else
                        {
                            return Ok("No Slots available right now! Try again later");
                        }

                    }
                    string MessageBody = null;


                    string[] MainBody = new string[TList.Count];
                    for ( int i = 0; i < TList.Count; i++)
                    {
                        MainBody[i] = getMultiplebody(i);
                    }


                    string getMultiplebody(int i )
                    {
                        
                            MessageBody = "Hospital Name: " + TList[i].Name + " Address: " + TList[i].Address
                               + " Pincode: " + TList[i].Pincode + " Available Slots: " + TList[i].AvailableCapacity + " Dose 1: " + TList[i].FirstDose
                               + " Dose 2: " + TList[i].SecondDose;

                       
                     return MessageBody;
                    }
                    TwilioClient.Init(AccountSid, AccountAuth);
                    var message = MessageResource.Create(
                           from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                           body: "Alerts by Kankan \n" + string.Join("\n", MainBody),
                           to: new Twilio.Types.PhoneNumber("whatsapp:+91"+enter.phonenumber));
                    return Ok(message.Sid);
                    //return Ok(TList[1]);


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
        //getState
        
        //getDistrict
        
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
