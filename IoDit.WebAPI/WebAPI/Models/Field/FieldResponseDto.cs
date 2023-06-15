using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.WebAPI.Models.Field;

public class FieldResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long CompanyId { get; set; }
    public long CompanyFarmId { get; set; }
    public Geometry Geofence { get; set; }
}