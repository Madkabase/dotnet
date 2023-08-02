using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.DTO.Threshold;

public class GlobalThresholdPresetDto : ThresholdDto
{
    public string Name { get; set; }

    // from Bo
    public static GlobalThresholdPresetDto FromBo(GlobalThresholdPresetBo globalThresholdPresetBo)
    {
        return new GlobalThresholdPresetDto
        {
            Id = globalThresholdPresetBo.Id,
            Name = globalThresholdPresetBo.Name,
            Humidity1Min = globalThresholdPresetBo.Humidity1Min,
            Humidity1Max = globalThresholdPresetBo.Humidity1Max,
            Humidity2Min = globalThresholdPresetBo.Humidity2Min,
            Humidity2Max = globalThresholdPresetBo.Humidity2Max,
            TemperatureMin = globalThresholdPresetBo.TemperatureMin,
            TemperatureMax = globalThresholdPresetBo.TemperatureMax,
            BatteryLevelMin = globalThresholdPresetBo.BatteryLevelMin,
            BatteryLevelMax = globalThresholdPresetBo.BatteryLevelMax,
        };
    }

}