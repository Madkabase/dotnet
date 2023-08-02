using IoDit.WebAPI.Persistence.Entities.Base;
using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.Persistence.Entities;

public class Field : EntityBase, IEntity
{
    public string Name { get; set; }
    public Farm Farm { get; set; }
    public Geometry Geofence { get; set; }
    public long ThresholdId { get; set; }
    public Threshold Threshold { get; set; }
    public ICollection<Device> Devices { get; set; } = new List<Device>();

}