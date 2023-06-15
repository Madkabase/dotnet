using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.WebAPI.Models.Field;

public class CreateCompanyFieldRequestDto
{
    public long CompanyUserId { get; set; }
    public string Name { get; set; }
    public Geometry Geofence { get; set; }
    public long CompanyFarmId { get; set; }
}