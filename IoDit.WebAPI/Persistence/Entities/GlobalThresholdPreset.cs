using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class GlobalThresholdPreset : EntityBase, IEntity
{
    public string Name { get; set; }
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public float Temperature1Min { get; set; }
    public float Temperature1Max { get; set; }
    public float Temperature2Min { get; set; }
    public float Temperature2Max { get; set; }
    public float Salinity1Min { get; set; }
    public float Salinity1Max { get; set; }
    public float Salinity2Min { get; set; }
    public float Salinity2Max { get; set; }

}