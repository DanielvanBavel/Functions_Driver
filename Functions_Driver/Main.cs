using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Functions_Driver
{
    public static class Main
    {
        [FunctionName("Main")]
        public static async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            //log.LogInformation($"Timer triggered function {await AddSensor.Run()} executed at: {DateTime.Now}");
            
        }
    }
}
