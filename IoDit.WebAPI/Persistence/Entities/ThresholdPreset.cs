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
    public int BatteryLevelMin { get; set; }
    public int BatteryLevelMax { get; set; }
    public double TemperatureMin { get; set; }
    public double TemperatureMax { get; set; }

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
            BatteryLevelMin = thresholdPreset.BatteryLevelMin,
            BatteryLevelMax = thresholdPreset.BatteryLevelMax,
            TemperatureMin = thresholdPreset.TemperatureMin,
            TemperatureMax = thresholdPreset.TemperatureMax
        };
    }
}