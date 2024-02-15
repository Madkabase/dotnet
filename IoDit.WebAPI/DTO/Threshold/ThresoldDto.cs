using IoDit.WebAPI.BO;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.Threshold
{
    public class ThresholdDto
    {
        public long Id { get; set; }
        public int Moisture1Min { get; set; }
        public int Moisture1Max { get; set; }
        public int Moisture2Min { get; set; }
        public int Moisture2Max { get; set; }
        public float Temperature1Min { get; set; }
        public float Temperature1Max { get; set; }
        public float Temperature2Min { get; set; }
        public float Temperature2Max { get; set; }
        public float Salinity1Min { get; set; }
        public float Salinity1Max { get; set; }
        public float Salinity2Min { get; set; }
        public float Salinity2Max { get; set; }
        public MainSensor MainSensor { get; set; }

        internal static ThresholdDto FromBo(ThresholdBo threshold)
        {
            return new ThresholdDto
            {
                Id = threshold.Id,
                Moisture1Min = threshold.Moisture1Min,
                Moisture1Max = threshold.Moisture1Max,
                Moisture2Min = threshold.Moisture2Min,
                Moisture2Max = threshold.Moisture2Max,
                Temperature1Min = threshold.Temperature1Min,
                Temperature1Max = threshold.Temperature1Max,
                Temperature2Min = threshold.Temperature2Min,
                Temperature2Max = threshold.Temperature2Max,
                Salinity1Min = threshold.Salinity1Min,
                Salinity1Max = threshold.Salinity1Max,
                Salinity2Min = threshold.Salinity2Min,
                Salinity2Max = threshold.Salinity2Max,
        MainSensor = threshold.MainSensor
    };
}
    }
}