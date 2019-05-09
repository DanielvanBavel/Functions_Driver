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
            log.LogInformation("Add Sensor function called");

            var result = "";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(new Sensor()
                {
                    DeviceName = "Device-"+ Helper.GenerateDevicename(),
                    DeviceModel = "KramlerBVF",
                    LocationName = "Amsterdam",
                    IsOnline = true
                });

                try
                {
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(Environment.GetEnvironmentVariable("LiveUri"), stringContent);

                   req.CreateResponse(HttpStatusCode.OK, response.ToString());
                }
                catch (HttpRequestException ex)
                {
                    req.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            return result;
        }
    }
}
