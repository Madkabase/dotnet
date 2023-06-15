using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.WebAPI.Models.Field;

public class CreateCompanyField
{
    public long CompanyUserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public Geometry Geofence { get; set; }
    public long CompanyFarmId { get; set; }
    public long CompanyId { get; set; }
}