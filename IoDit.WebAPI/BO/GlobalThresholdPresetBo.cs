
namespace IoDit.WebAPI.BO;

public class GlobalThresholdPresetBo
{
    public long Id { get; set; }
    public string Name { get; set; }
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

    public GlobalThresholdPresetBo()
    {
        Id = 0;
        Name = "";
        Moisture1Min = 0;
        Moisture1Max = 0;
        Moisture2Min = 0;
        Moisture2Max = 0;
        Temperature1Min = 0;
        Temperature1Max = 0;
        Temperature2Min = 0;
        Temperature2Max = 0;
        Salinity1Min = 0;
        Salinity1Max = 0;
        Salinity2Min = 0;
        Salinity2Max = 0;
    }

    public GlobalThresholdPresetBo(long id, string name, int humidity1Min, int humidity1Max, int humidity2Min, int humidity2Max, float temperature1Min, float temperature1Max, float temperature2Min, float temperature2Max, float salinity1Min, float salinity1Max, float salinity2Min, float salinity2Max)
    {
        Id = id;
        Name = name;
        Moisture1Min = humidity1Min;
        Moisture1Max = humidity1Max;
        Moisture2Min = humidity2Min;
        Moisture2Max = humidity2Max;
        Temperature1Min = temperature1Min;
        Temperature1Max = temperature1Max;
        Temperature2Min = temperature2Min;
        Temperature2Max = temperature2Max;
        Salinity1Min = salinity1Min;
        Salinity1Max = salinity1Max;
        Salinity2Min = salinity2Min;
        Salinity2Max = salinity2Max;
    }

    //from Dto
    public static GlobalThresholdPresetBo FromDto(DTO.Threshold.GlobalThresholdPresetDto globalThresholdPresetDto)
    {
        return new GlobalThresholdPresetBo
        {
            Id = globalThresholdPresetDto.Id,
            Name = globalThresholdPresetDto.Name,
            Moisture1Min = globalThresholdPresetDto.Moisture1Min,
            Moisture1Max = globalThresholdPresetDto.Moisture1Max,
            Moisture2Min = globalThresholdPresetDto.Moisture2Min,
            Moisture2Max = globalThresholdPresetDto.Moisture2Max,
            Temperature1Min = globalThresholdPresetDto.Temperature1Min,
            Temperature1Max = globalThresholdPresetDto.Temperature1Max,
            Temperature2Min = globalThresholdPresetDto.Temperature2Min,
            Temperature2Max = globalThresholdPresetDto.Temperature2Max,
            Salinity1Min = globalThresholdPresetDto.Salinity1Min,
            Salinity1Max = globalThresholdPresetDto.Salinity1Max,
            Salinity2Min = globalThresholdPresetDto.Salinity2Min,
            Salinity2Max = globalThresholdPresetDto.Salinity2Max
        };
    }

    // from Entity
    public static GlobalThresholdPresetBo FromEntity(Persistence.Entities.GlobalThresholdPreset globalThresholdPreset)
    {
        return new GlobalThresholdPresetBo
        {
            Id = globalThresholdPreset.Id,
            Name = globalThresholdPreset.Name,
            Moisture1Min = globalThresholdPreset.Moisture1Min,
            Moisture1Max = globalThresholdPreset.Moisture1Max,
            Moisture2Min = globalThresholdPreset.Moisture2Min,
            Moisture2Max = globalThresholdPreset.Moisture2Max,
            Temperature1Min = globalThresholdPreset.Temperature1Min,
            Temperature1Max = globalThresholdPreset.Temperature1Max,
            Temperature2Min = globalThresholdPreset.Temperature2Min,
            Temperature2Max = globalThresholdPreset.Temperature2Max,
            Salinity1Min = globalThresholdPreset.Salinity1Min,
            Salinity1Max = globalThresholdPreset.Salinity1Max,
            Salinity2Min = globalThresholdPreset.Salinity2Min,
            Salinity2Max = globalThresholdPreset.Salinity2Max
        };
    }
}