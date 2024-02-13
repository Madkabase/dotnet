using IoDit.WebAPI.BO;
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
        public double Temperature1Min { get; set; }
        public double Temperature1Max { get; set; }
        public double Temperature2Min { get; set; }
        public double Temperature2Max { get; set; }
        public MainSensor MainSensor { get; set; }

        internal static ThresholdDto FromBo(ThresholdBo threshold)
        {
            return new ThresholdDto
            {
                Id = threshold.Id,
                Humidity1Min = threshold.Humidity1Min,
                Humidity1Max = threshold.Humidity1Max,
                Humidity2Min = threshold.Humidity2Min,
                Humidity2Max = threshold.Humidity2Max,
                Temperature1Min = threshold.Temperature1Min,
                Temperature1Max = threshold.Temperature1Max,
                Temperature2Min = threshold.Temperature2Min,
                Temperature2Max = threshold.Temperature2Max,
                MainSensor = threshold.MainSensor
            };
        }
    }
}