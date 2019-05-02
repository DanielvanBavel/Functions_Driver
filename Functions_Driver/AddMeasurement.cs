using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RandomType;
using System;
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
            log.LogInformation("Add measurement function called");

            var result = "";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                int randomTemp = RandomTypeGenerator.Generate<int>(settings =>
                {
                    settings.Min.Int32 = -127;
                    settings.Max.Int32 = 57;
                });

                var json = JsonConvert.SerializeObject(new Measurement()
                {
                    Temperature = randomTemp,
                    MeasureDate = DateTime.Now.ToString("dd-MM-yyyy"),
                    MeasureTime = DateTime.Now.ToString("HH:mm")
                });

                HttpRequestMessage msg = new HttpRequestMessage(new HttpMethod("POST"), Environment.GetEnvironmentVariable("LiveUri"));

                try
                {
                    var id = 17;

                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(Environment.GetEnvironmentVariable("LiveUri") + id + "/measurement", stringContent);

                    result = response.ToString();
                    req.CreateResponse(HttpStatusCode.OK, response);
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