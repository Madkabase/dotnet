using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class GlobalThresholdPreset : EntityBase, IEntity
{
    public string Name { get; set; }
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public double Temperature1Min { get; set; }
    public double Temperature1Max { get; set; }
    public double Temperature2Min { get; set; }
    public double Temperature2Max { get; set; }
}