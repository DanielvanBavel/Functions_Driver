namespace Functions_Driver
{
    public class Sensor
    {
        /// <summary>
        /// DeviceName property of the sensor
        /// <value>a combination of device- with a extra generated random string</value>
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// DeviceModel property of the sensor
        /// <value></value>
        /// </summary>
        public string DeviceModel { get; set; }

        /// <summary>
        /// LocationName property of the sensor
        /// <value></value>
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// IsOnline property of the sensor
        /// <value>True/False</value>
        /// </summary>
        public bool IsOnline { get; set; }
    }
}
