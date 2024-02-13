using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class ThresholdPreset : EntityBase, IEntity
{
    public string Name { get; set; }
    public long FarmId { get; set; }
    public Farm Farm { get; set; }
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public double Temperature1Min { get; set; }
    public double Temperature1Max { get; set; }
    public double Temperature2Min { get; set; }
    public double Temperature2Max { get; set; }

    internal static ThresholdPreset FromBo(ThresholdPresetBo thresholdPreset)
    {
        return new()
        {
            Name = thresholdPreset.Name,
            FarmId = thresholdPreset.Farm.Id,
            Humidity1Min = thresholdPreset.Humidity1Min,
            Humidity1Max = thresholdPreset.Humidity1Max,
            Humidity2Min = thresholdPreset.Humidity2Min,
            Humidity2Max = thresholdPreset.Humidity2Max,
            Temperature1Min = thresholdPreset.Temperature1Min,
            Temperature1Max = thresholdPreset.Temperature1Max,
            Temperature2Min = thresholdPreset.Temperature2Min,
            Temperature2Max = thresholdPreset.Temperature2Max
        };
    }
}