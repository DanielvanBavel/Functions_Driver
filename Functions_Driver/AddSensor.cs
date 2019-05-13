using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Functions_Driver
{
    public static class AddSensor
    {
        [FunctionName("AddSensor")]
        public static async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req, ILogger log)
        {
            // Log information
            log.LogInformation("Add Sensor function called");

            // create empty string
            var result = "";

            //Creates a new HttpClient instance
            using (HttpClient httpClient = new HttpClient())
            {
                //Add headers to client to accept json data.
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Creates a new Temperature device
                var json = JsonConvert.SerializeObject(new Sensor()
                {
                    DeviceName = "Device-"+ Helper.GenerateDevicename(),
                    DeviceModel = "KramlerBVF",
                    LocationName = "Amsterdam",
                    IsOnline = true
                });

                // try to post the json data to the RestAPI
                try
                {
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(Environment.GetEnvironmentVariable("LiveUri"), stringContent);

                    result = response.ToString();
                    req.CreateResponse(HttpStatusCode.OK, result);
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
