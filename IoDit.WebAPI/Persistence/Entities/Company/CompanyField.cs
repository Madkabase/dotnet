using IoDit.WebAPI.Persistence.Entities.Base;
using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.Persistence.Entities.Company;

public class CompanyField : EntityBase, IEntity
{
    public string Name { get; set; }
    public Geometry Geofence { get; set; }
    public long CompanyFarmId { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public CompanyFarm CompanyFarm { get; set; }
    public ICollection<CompanyDevice> Devices { get; set; } = new List<CompanyDevice>();

}