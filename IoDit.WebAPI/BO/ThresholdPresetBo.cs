using IoDit.WebAPI.DTO.Threshold;

namespace IoDit.WebAPI.BO;

public class ThresholdPresetBo : GlobalThresholdPresetBo
{

    public FarmBo Farm { get; set; }

    public ThresholdPresetBo()
    {
        Id = 0;
        Name = "";
        Farm = new FarmBo();
        Humidity1Min = 0;
        Humidity1Max = 0;
        Humidity2Min = 0;
        Humidity2Max = 0;
        BatteryLevelMin = 0;
        BatteryLevelMax = 0;
        TemperatureMin = 0;
        TemperatureMax = 0;
    }

    public ThresholdPresetBo(long id, string name, FarmBo farm, int humidity1Min, int humidity1Max, int humidity2Min, int humidity2Max, int batteryLevelMin, int batteryLevelMax, double temperatureMin, double temperatureMax)
    {
        Id = id;
        Name = name;
        Farm = farm;
        Humidity1Min = humidity1Min;
        Humidity1Max = humidity1Max;
        Humidity2Min = humidity2Min;
        Humidity2Max = humidity2Max;
        BatteryLevelMin = batteryLevelMin;
        BatteryLevelMax = batteryLevelMax;
        TemperatureMin = temperatureMin;
        TemperatureMax = temperatureMax;
    }

    public static ThresholdPresetBo FromDto(ThresholdPresetDto thresholdPresetDto)
    {
        return new ThresholdPresetBo
        {
            Id = thresholdPresetDto.Id,
            Name = thresholdPresetDto.Name,
            Humidity1Min = thresholdPresetDto.Humidity1Min,
            Humidity1Max = thresholdPresetDto.Humidity1Max,
            Humidity2Min = thresholdPresetDto.Humidity2Min,
            Humidity2Max = thresholdPresetDto.Humidity2Max,
            BatteryLevelMin = thresholdPresetDto.BatteryLevelMin,
            BatteryLevelMax = thresholdPresetDto.BatteryLevelMax,
            TemperatureMin = thresholdPresetDto.TemperatureMin,
            TemperatureMax = thresholdPresetDto.TemperatureMax
        };
    }

    public static ThresholdPresetBo FromEntity(Persistence.Entities.ThresholdPreset thresholdPreset)
    {
        return new ThresholdPresetBo
        {
            Id = thresholdPreset.Id,
            Name = thresholdPreset.Name,
            Farm = new FarmBo(),
            Humidity1Min = thresholdPreset.Humidity1Min,
            Humidity1Max = thresholdPreset.Humidity1Max,
            Humidity2Min = thresholdPreset.Humidity2Min,
            Humidity2Max = thresholdPreset.Humidity2Max,
            BatteryLevelMin = thresholdPreset.BatteryLevelMin,
            BatteryLevelMax = thresholdPreset.BatteryLevelMax,
            TemperatureMin = thresholdPreset.TemperatureMin,
            TemperatureMax = thresholdPreset.TemperatureMax
        };
    }

}