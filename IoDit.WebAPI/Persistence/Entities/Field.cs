using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities.Base;
using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.Persistence.Entities;

public class Field : EntityBase, IEntity
{
    public string Name { get; set; }
    public long FarmId { get; set; }
    public Farm Farm { get; set; }
    public Geometry Geofence { get; set; }
    public long ThresholdId { get; set; }
    public Threshold Threshold { get; set; }
    public ICollection<Device> Devices { get; set; } = new List<Device>();


    public static Field FromBo(FieldBo field)
    {
        return new Field
        {
            Id = field.Id,
            Name = field.Name,
            Geofence = field.Geofence,
            Threshold = field.Threshold != null ? Threshold.FromBo(field.Threshold) : new Threshold()
        };
    }
}