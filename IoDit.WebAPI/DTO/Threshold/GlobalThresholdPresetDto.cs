namespace IoDit.WebAPI.DTO.Threshold;

public class GlobalThresholdPresetDto : ThresholdDto
{
    public string Name { get; set; }

    // from entity
    internal static GlobalThresholdPresetDto FromEntity(Persistence.Entities.GlobalThresholdPreset globalThresholdPreset)
    {
        return new GlobalThresholdPresetDto
        {
            Id = globalThresholdPreset.Id,
            Name = globalThresholdPreset.Name,
            Humidity1Min = globalThresholdPreset.Humidity1Min,
            Humidity1Max = globalThresholdPreset.Humidity1Max,
            Humidity2Min = globalThresholdPreset.Humidity2Min,
            Humidity2Max = globalThresholdPreset.Humidity2Max,
            TemperatureMin = globalThresholdPreset.TemperatureMin,
            TemperatureMax = globalThresholdPreset.TemperatureMax,
            BatteryLevelMin = globalThresholdPreset.BatteryLevelMin,
            BatteryLevelMax = globalThresholdPreset.BatteryLevelMax
        };
    }
}