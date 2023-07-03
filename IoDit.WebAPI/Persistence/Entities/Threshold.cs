using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Entities;

public class Threshold : EntityBase, IEntity
{
    public Field? Field { get; set; } = null;
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public double TemperatureMin { get; set; }
    public double TemperatureMax { get; set; }
    public int BatteryLevelMin { get; set; }
    public int BatteryLevelMax { get; set; }
}