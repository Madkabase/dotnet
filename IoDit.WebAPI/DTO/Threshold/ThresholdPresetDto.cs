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
            Moisture1Max = thresholdPresetBo.Moisture1Max,
            Moisture1Min = thresholdPresetBo.Moisture1Min,
            Moisture2Max = thresholdPresetBo.Moisture2Max,
            Moisture2Min = thresholdPresetBo.Moisture2Min,
            Temperature1Max = thresholdPresetBo.Temperature1Max,
            Temperature1Min = thresholdPresetBo.Temperature1Min,
            Temperature2Max = thresholdPresetBo.Temperature2Max,
            Temperature2Min = thresholdPresetBo.Temperature2Min
        };
    }
}