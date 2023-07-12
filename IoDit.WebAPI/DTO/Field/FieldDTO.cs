using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.DTO.User;
using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.DTO.Field;

public class FieldDto
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public Geometry? Geofence { get; set; } = null;
    public List<DeviceDto> Devices { get; set; } = new List<DeviceDto>();

    public ThresholdDto? Threshold { get; set; } = null;
    public long OverallMoistureLevel { get; set; } = 0;

    public static FieldDto FromEntity(Persistence.Entities.Field field)
    {
        return new FieldDto
        {
            Id = field.Id,
            Name = field.Name,
            Geofence = field.Geofence,
            Devices = field.Devices.Select(d => DeviceDto.FromEntity(d)).ToList()
        };
    }

}
