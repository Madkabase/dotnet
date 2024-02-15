using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class ThresholdPreset : EntityBase, IEntity
{
    public string Name { get; set; }
    public long FarmId { get; set; }
    public Farm Farm { get; set; }
    public int Moisture1Min { get; set; }
    public int Moisture1Max { get; set; }
    public int Moisture2Min { get; set; }
    public int Moisture2Max { get; set; }
    public float Temperature1Min { get; set; }
    public float Temperature1Max { get; set; }
    public float Temperature2Min { get; set; }
    public float Temperature2Max { get; set; }

    internal static ThresholdPreset FromBo(ThresholdPresetBo thresholdPreset)
    {
        return new()
        {
            Name = thresholdPreset.Name,
            FarmId = thresholdPreset.Farm.Id,
            Moisture1Min = thresholdPreset.Moisture1Min,
            Moisture1Max = thresholdPreset.Moisture1Max,
            Moisture2Min = thresholdPreset.Moisture2Min,
            Moisture2Max = thresholdPreset.Moisture2Max,
            Temperature1Min = thresholdPreset.Temperature1Min,
            Temperature1Max = thresholdPreset.Temperature1Max,
            Temperature2Min = thresholdPreset.Temperature2Min,
            Temperature2Max = thresholdPreset.Temperature2Max
        };
    }
}