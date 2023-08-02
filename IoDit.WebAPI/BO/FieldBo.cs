using IoDit.WebAPI.Persistence.Entities;
using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.BO;

public class FieldBo
{
    public string Name { get; set; }
    public FarmBo Farm { get; set; }
    public Geometry Geofence { get; set; }
    public long? ThresholdId { get; set; }
    public ThresholdBo? Threshold { get; set; }
    public ICollection<DeviceBo> Devices { get; set; } = new List<DeviceBo>();

    public FieldBo()
    {
        Name = "";
        Farm = new FarmBo();
        Geofence = new Point(0, 0);
        ThresholdId = null;
        Threshold = null;
        Devices = new List<DeviceBo>();
    }
    public FieldBo(string name, FarmBo farm, Geometry geofence, long? thresholdId, ThresholdBo? threshold, ICollection<DeviceBo> devices)
    {
        Name = name;
        Farm = farm;
        Geofence = geofence;
        ThresholdId = thresholdId;
        Threshold = threshold;
        Devices = devices;
    }

    //from entity
    public static FieldBo FromEntity(Field entity)
    {
        return new FieldBo(
            entity.Name,
            FarmBo.FromEntity(entity.Farm),
            entity.Geofence,
            entity.ThresholdId,
            ThresholdBo.FromEntity(entity.Threshold),
            entity.Devices.Select(d => DeviceBo.FromEntity(d)).ToList()
        );
    }


}