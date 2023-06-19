using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class ThresholdPreset : EntityBase, IEntity
{
    public string Name { get; set; }
    public Farm Farm { get; set; }
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public int BatteryLevelMin { get; set; }
    public int BatteryLevelMax { get; set; }
    public double TemperatureMin { get; set; }
    public double TemperatureMax { get; set; }
}