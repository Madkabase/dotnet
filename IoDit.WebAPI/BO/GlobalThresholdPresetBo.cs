
namespace IoDit.WebAPI.BO;

public class GlobalThresholdPresetBo
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public double Temperature1Min { get; set; }
    public double Temperature1Max { get; set; }
    public double Temperature2Min { get; set; }
    public double Temperature2Max { get; set; }

    public GlobalThresholdPresetBo()
    {
        Id = 0;
        Name = "";
        Humidity1Min = 0;
        Humidity1Max = 0;
        Humidity2Min = 0;
        Humidity2Max = 0;
        Temperature1Min = 0;
        Temperature1Max = 0;
        Temperature2Min = 0;
        Temperature2Max = 0;
    }

    public GlobalThresholdPresetBo(long id, string name, int humidity1Min, int humidity1Max, int humidity2Min, int humidity2Max, double temperature1Min, double temperature1Max, double temperature2Min, double temperature2Max)
    {
        Id = id;
        Name = name;
        Humidity1Min = humidity1Min;
        Humidity1Max = humidity1Max;
        Humidity2Min = humidity2Min;
        Humidity2Max = humidity2Max;
        Temperature1Min = temperature1Min;
        Temperature1Max = temperature1Max;
        Temperature2Min = temperature2Min;
        Temperature2Max = temperature2Max;
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
            Temperature1Min = globalThresholdPresetDto.Temperature1Min,
            Temperature1Max = globalThresholdPresetDto.Temperature1Max,
            Temperature2Min = globalThresholdPresetDto.Temperature2Min,
            Temperature2Max = globalThresholdPresetDto.Temperature2Max
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
            Temperature1Min = globalThresholdPreset.Temperature1Min,
            Temperature1Max = globalThresholdPreset.Temperature1Max,
            Temperature2Min = globalThresholdPreset.Temperature2Min,
            Temperature2Max = globalThresholdPreset.Temperature2Max
        };
    }
}