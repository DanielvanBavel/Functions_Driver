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
            //Set some environment variables
            var baseUrl = Environment.GetEnvironmentVariable("FunctionBaseApi");
            var sensorCall = Environment.GetEnvironmentVariable("FunctionCallSensor");
            var MeasurementCall = Environment.GetEnvironmentVariable("FunctionCallMeasurement");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //set the base address of the httpclient
                    client.BaseAddress = new Uri(baseUrl);

                    //create emtpy KeyValuePair content variable
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("", "")
                    });

                    //Call the azure function addSensor API
                    await client.PostAsync(sensorCall, content);

                    //Call the azure function AddMeasurement API
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
