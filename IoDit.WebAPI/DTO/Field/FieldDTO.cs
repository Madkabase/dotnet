using IoDit.WebAPI.BO;
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

    public ThresholdDto Threshold { get; set; }
    public float OverallMoistureLevel { get; set; } = 0;
    public bool IsRequesterAdmin { get; set; } = false;

    internal static FieldDto FromBo(FieldBo f)
    {
        return new FieldDto
        {
            Id = f.Id,
            Name = f.Name,
            Geofence = f.Geofence,
            Threshold = ThresholdDto.FromBo(f.Threshold),
            Devices = f.Devices.Select(d => DeviceDto.FromBo(d)).ToList(),
            OverallMoistureLevel = 0
        };
    }
}
