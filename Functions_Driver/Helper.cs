using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RandomType;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Functions_Driver
{
    public static class Helper
    {
        /// <summary>
        /// Generates a extra string part for the device name to be more unique.
        /// </summary>
        /// <returns>Returns random string like: 64hnfcu</returns>
        public static string GenerateDevicename()
        {
            string deviceName = RandomTypeGenerator.Generate<string>(settings =>
            {
                settings.Min.String = 5;
                settings.Max.String = 20;
            });

            deviceName = deviceName.Replace(" ", "");

            return deviceName;
        }

        /// <summary>
        /// Generates a temperature value as an Integer.
        /// </summary>
        /// <returns>random int between -127 and +57</returns>
        public static int GenerateTemperature()
        {
            int randomTemp = RandomTypeGenerator.Generate<int>(settings =>
            {
                settings.Min.Int32 = -127;
                settings.Max.Int32 = 57;
            });

            return randomTemp;
        }

        /// <summary>
        /// Calls API root to query all the current Sensor objects
        /// </summary>
        /// <returns>List of sensorId's of all sensors</returns>
        public static async Task<List<int>> GetListOfSensorIds()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            List<int> sensorIds = new List<int>();

            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(Environment.GetEnvironmentVariable("LiveUri"));

                var result = response.Content.ReadAsStringAsync().Result;
                JArray items = (JArray)JsonConvert.DeserializeObject(result);

                foreach (var item in items)
                {
                    var tempId = item["temperatureSensorId"];
                    int id = Int32.Parse(tempId.ToString());
                    sensorIds.Add(id);
                }
            }
            return sensorIds;
        }
    }
}
