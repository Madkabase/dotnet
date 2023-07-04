namespace IoDit.WebAPI.DTO.Threshold
{
    public class ThresoldDto
    {
        public long Id { get; set; }
        public int Humidity1Min { get; set; }
        public int Humidity1Max { get; set; }
        public int Humidity2Min { get; set; }
        public int Humidity2Max { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public int BatteryLevelMin { get; set; }
        public int BatteryLevelMax { get; set; }

    }
}