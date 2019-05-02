using RandomType;

namespace Functions_Driver
{
    public static class Helper
    {
        public static string GenerateDevicename()
        {
            string deviceNameId = RandomTypeGenerator.Generate<string>(settings =>
            {
                settings.Min.String = 5;
                settings.Max.String = 10;
            });

            return deviceNameId;
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

        public static int GetSensorId()
        {
            return 17;
        }
    }
}
