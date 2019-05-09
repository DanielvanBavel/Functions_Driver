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
        public static string GenerateDevicename()
        {
            string deviceName = RandomTypeGenerator.Generate<string>(settings =>
            {
                settings.Min.String = 5;
                settings.Max.String = 10;
            });

            deviceName = deviceName.Replace(" ", "");

            return deviceName;
        }

        public static int GenerateTemperature()
        {
            int randomTemp = RandomTypeGenerator.Generate<int>(settings =>
            {
                settings.Min.Int32 = -127;
                settings.Max.Int32 = 57;
            });

            return randomTemp;
        }

        public static async Task<List<int>> getListOfSensorIds()
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
