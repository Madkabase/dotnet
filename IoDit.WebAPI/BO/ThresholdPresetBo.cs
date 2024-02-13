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
        Temperature1Min = 0;
        Temperature1Max = 0;
        Temperature2Min = 0;
        Temperature2Max = 0;
    }

    public ThresholdPresetBo(long id, string name, FarmBo farm, int humidity1Min, int humidity1Max, int humidity2Min, int humidity2Max, float temperature1Min, float temperature1Max, float temperature2Min, float temperature2Max)
    {
        Id = id;
        Name = name;
        Farm = farm;
        Humidity1Min = humidity1Min;
        Humidity1Max = humidity1Max;
        Humidity2Min = humidity2Min;
        Humidity2Max = humidity2Max;
        Temperature1Min = temperature1Min;
        Temperature1Max = temperature1Max;
        Temperature2Min = temperature2Min;
        Temperature2Max = temperature2Max;
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
            Temperature1Min = thresholdPresetDto.Temperature1Min,
            Temperature1Max = thresholdPresetDto.Temperature1Max,
            Temperature2Min = thresholdPresetDto.Temperature2Min,
            Temperature2Max = thresholdPresetDto.Temperature2Max,
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
            Temperature1Min = thresholdPreset.Temperature1Min,
            Temperature1Max = thresholdPreset.Temperature1Max,
            Temperature2Min = thresholdPreset.Temperature2Min,
            Temperature2Max = thresholdPreset.Temperature2Max,
        };
    }

}