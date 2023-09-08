
namespace IoDit.WebAPI.BO;

public class GlobalThresholdPresetBo
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public int BatteryLevelMin { get; set; }
    public int BatteryLevelMax { get; set; }
    public double TemperatureMin { get; set; }
    public double TemperatureMax { get; set; }

    public GlobalThresholdPresetBo()
    {
        Id = 0;
        Name = "";
        Humidity1Min = 0;
        Humidity1Max = 0;
        Humidity2Min = 0;
        Humidity2Max = 0;
        BatteryLevelMin = 0;
        BatteryLevelMax = 0;
        TemperatureMin = 0;
        TemperatureMax = 0;
    }

    public GlobalThresholdPresetBo(long id, string name, int humidity1Min, int humidity1Max, int humidity2Min, int humidity2Max, int batteryLevelMin, int batteryLevelMax, double temperatureMin, double temperatureMax)
    {
        Id = id;
        Name = name;
        Humidity1Min = humidity1Min;
        Humidity1Max = humidity1Max;
        Humidity2Min = humidity2Min;
        Humidity2Max = humidity2Max;
        BatteryLevelMin = batteryLevelMin;
        BatteryLevelMax = batteryLevelMax;
        TemperatureMin = temperatureMin;
        TemperatureMax = temperatureMax;
    }

    //from Dto
    public static GlobalThresholdPresetBo FromDto(DTO.Threshold.GlobalThresholdPresetDto globalThresholdPresetDto)
    {
        return new GlobalThresholdPresetBo
        {
            Id = globalThresholdPresetDto.Id,
            Name = globalThresholdPresetDto.Name,
            Humidity1Min = globalThresholdPresetDto.Humidity1Min,
            Humidity1Max = globalThresholdPresetDto.Humidity1Max,
            Humidity2Min = globalThresholdPresetDto.Humidity2Min,
            Humidity2Max = globalThresholdPresetDto.Humidity2Max,
            BatteryLevelMin = globalThresholdPresetDto.BatteryLevelMin,
            BatteryLevelMax = globalThresholdPresetDto.BatteryLevelMax,
            TemperatureMin = globalThresholdPresetDto.TemperatureMin,
            TemperatureMax = globalThresholdPresetDto.TemperatureMax
        };
    }

    // from Entity
    public static GlobalThresholdPresetBo FromEntity(Persistence.Entities.GlobalThresholdPreset globalThresholdPreset)
    {
        return new GlobalThresholdPresetBo
        {
            Id = globalThresholdPreset.Id,
            Name = globalThresholdPreset.Name,
            Humidity1Min = globalThresholdPreset.Humidity1Min,
            Humidity1Max = globalThresholdPreset.Humidity1Max,
            Humidity2Min = globalThresholdPreset.Humidity2Min,
            Humidity2Max = globalThresholdPreset.Humidity2Max,
            BatteryLevelMin = globalThresholdPreset.BatteryLevelMin,
            BatteryLevelMax = globalThresholdPreset.BatteryLevelMax,
            TemperatureMin = globalThresholdPreset.TemperatureMin,
            TemperatureMax = globalThresholdPreset.TemperatureMax
        };
    }
}