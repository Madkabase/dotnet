using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.WebAPI.Models.Field;

public class UpdateGeofenceRequestDto
{
    public Geometry Geofence { get; set; }
    public long CompanyUserId { get; set; }
    public long CompanyFarmId { get; set; }
    public long FieldId { get; set; }
}