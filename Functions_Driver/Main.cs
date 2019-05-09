using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Functions_Driver
{
    public static class Main
    {
        /*
         * Method runs every 5 minutes
         */ 
        [FunctionName("Main")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {


            var baseUrl = Environment.GetEnvironmentVariable("FunctionBaseApi");
            var sensorCall = Environment.GetEnvironmentVariable("FunctionCallSensor");
            var MeasurementCall = Environment.GetEnvironmentVariable("FunctionCallMeasurement");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("", "")
                    });

                    //addSensor
                    await client.PostAsync(sensorCall, content);

                    //addMeasurement
                    await client.PostAsync(MeasurementCall, content);
                }
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.ToString());
            }
        }
    }
}
