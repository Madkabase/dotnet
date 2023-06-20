using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.DTO.Field;

public class FieldDto
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public Geometry Geofence { get; set; } = null!;

    public static FieldDto FromEntity(Persistence.Entities.Field field)
    {
        return new FieldDto
        {
            Id = field.Id,
            Name = field.Name,
            Geofence = field.Geofence
        };
    }

}
