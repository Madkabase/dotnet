using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.Threshold
{
    public class ThresholdDto
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
        public MainSensor MainSensor { get; set; }

        internal static ThresholdDto FromEntity(Persistence.Entities.Threshold threshold)
        {
            return new ThresholdDto
            {
                Id = threshold.Id,
                Humidity1Min = threshold.Humidity1Min,
                Humidity1Max = threshold.Humidity1Max,
                Humidity2Min = threshold.Humidity2Min,
                Humidity2Max = threshold.Humidity2Max,
                TemperatureMin = threshold.TemperatureMin,
                TemperatureMax = threshold.TemperatureMax,
                BatteryLevelMin = threshold.BatteryLevelMin,
                BatteryLevelMax = threshold.BatteryLevelMax,
                MainSensor = threshold.MainSensor
            };
        }
    }
}