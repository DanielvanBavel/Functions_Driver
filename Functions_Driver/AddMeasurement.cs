using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Functions_Driver
{
    public static class AddMeasurement
    {
        [FunctionName("AddMeasurement")]
        public static async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            // log information
            log.LogInformation("Add measurement function called");

            //create emtpy string
            var result = "";

            //create new httpClient instance
            using (HttpClient httpClient = new HttpClient())
            {
                //Add headers to client to accept json data.
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    //Get the list of sensor id's by calling the helper function
                    List<int> sensorIdList = await Helper.GetListOfSensorIds();

                    //check if the sensorList have some items, otherwise do nothing
                    //bcz if there are no sensors, measurement cannot be added.
                    if (sensorIdList.Count != 0)
                    {
                        // if there are items loop through it.
                        foreach (int id in sensorIdList)
                        {
                            //create json string from the Measurement object
                            var json = JsonConvert.SerializeObject(new Measurement()
                            {
                                Temperature = Helper.GenerateTemperature(),
                                MeasureDate = DateTime.Now.ToString("dd-MM-yyyy"),
                                MeasureTime = DateTime.Now.ToString("HH:mm")
                            });

                            //Post request to the Rest API
                            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                            var response = await httpClient.PostAsync(Environment.GetEnvironmentVariable("LiveUri") + id + "/measurement", stringContent);

                            //give the response
                            result = response.ToString();
                            req.CreateResponse(HttpStatusCode.OK, result);
                        }
                    }                                           
                }
                catch (HttpRequestException ex)
                {
                    //catch the exception
                    req.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            return result;
        }
    }
}
