//using Microsoft.Azure.WebJobs;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.IO;

//namespace Functions_Driver
//{
//    public static class GetSensorId
//    {
//        [FunctionName("GetSensorId")]
//        public static int Run()
//        {
//            var path = Directory.GetCurrentDirectory() + "/../../../Data/result.json";

//            StreamReader file = File.OpenText(path);
//            JsonTextReader reader = new JsonTextReader(file);
//            JObject o2 = (JObject)JToken.ReadFrom(reader);

//            string sid = o2.GetValue("temperatureSensorId").ToString();
//            int id = int.Parse(sid);

//            return id;
//        }
//    }
//}
