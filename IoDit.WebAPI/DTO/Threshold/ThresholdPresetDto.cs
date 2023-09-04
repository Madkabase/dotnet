using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Farm;

namespace IoDit.WebAPI.DTO.Threshold
{
    public class ThresholdPresetDto : ThresholdDto
    {
        public string Name { get; set; }

        public static ThresholdPresetDto FromBo(ThresholdPresetBo thresholdPresetBo) => new()
        {
            Id = thresholdPresetBo.Id,
            Name = thresholdPresetBo.Name,
            BatteryLevelMax = thresholdPresetBo.BatteryLevelMax,
            BatteryLevelMin = thresholdPresetBo.BatteryLevelMin,
            Humidity1Max = thresholdPresetBo.Humidity1Max,
            Humidity1Min = thresholdPresetBo.Humidity1Min,
            Humidity2Max = thresholdPresetBo.Humidity2Max,
            Humidity2Min = thresholdPresetBo.Humidity2Min,
            TemperatureMax = thresholdPresetBo.TemperatureMax,
            TemperatureMin = thresholdPresetBo.TemperatureMin
        };
    }
}