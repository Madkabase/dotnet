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
            Moisture1Min = globalThresholdPresetBo.Moisture1Min,
            Moisture1Max = globalThresholdPresetBo.Moisture1Max,
            Moisture2Min = globalThresholdPresetBo.Moisture2Min,
            Moisture2Max = globalThresholdPresetBo.Moisture2Max,
            Temperature1Min = globalThresholdPresetBo.Temperature1Min,
            Temperature1Max = globalThresholdPresetBo.Temperature1Max,
            Temperature2Min = globalThresholdPresetBo.Temperature2Min,
            Temperature2Max = globalThresholdPresetBo.Temperature2Max,
        };
    }

}