using IoDit.WebAPI.Persistence.Entities;
using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.BO;

public class FieldBo
{
    public long Id { get; set; }
    public string Name { get; set; }
    // public FarmBo Farm { get; set; }
    public Geometry Geofence { get; set; }
    public long? ThresholdId { get; set; }
    public ThresholdBo? Threshold { get; set; }
    public ICollection<DeviceBo> Devices { get; set; } = new List<DeviceBo>();

    public FieldBo()
    {
        Id = 0;
        Name = "";
        // Farm = new FarmBo();
        Geofence = new Point(0, 0);
        ThresholdId = null;
        Threshold = null;
        Devices = new List<DeviceBo>();
    }
    public FieldBo(long id, string name, /*FarmBo farm,*/ Geometry geofence, long? thresholdId, ThresholdBo? threshold, ICollection<DeviceBo> devices)
    {
        Id = id;
        Name = name;
        // Farm = farm;
        Geofence = geofence;
        ThresholdId = thresholdId;
        Threshold = threshold;
        Devices = devices;
    }

    //from entity
    public static FieldBo FromEntity(Field entity)
    {
        return new FieldBo(
            entity.Id,
            entity.Name,
            // FarmBo.FromEntity(entity.Farm),
            entity.Geofence,
            entity.ThresholdId,
            ThresholdBo.FromEntity(entity.Threshold ?? new Threshold() { Id = entity.ThresholdId }),
            entity.Devices.Select(DeviceBo.FromEntity).ToList()
        );
    }

    //from dto
    public static FieldBo FromDto(DTO.Field.FieldDto dto)
    {
        return new FieldBo(
            dto.Id,
            dto.Name ?? "",
            // FarmBo.FromDto(dto.Farm),
            dto.Geofence ?? new Point(0, 0),
            dto.Threshold?.Id ?? 0,
            dto.Threshold != null ? ThresholdBo.FromDto(dto.Threshold) : null,
            dto.Devices != null ? dto.Devices.Select(d => DeviceBo.FromDTO(d)).ToList() : new List<DeviceBo>()
        );
    }


}