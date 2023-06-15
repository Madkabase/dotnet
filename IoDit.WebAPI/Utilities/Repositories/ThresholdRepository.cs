using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities.Company;

namespace IoDit.WebAPI.Utilities.Repositories;

public class ThresholdRepository : IThresholdRepository

{
    public IoDitDbContext DbContext { get; }

    public ThresholdRepository(IoDitDbContext context)
    {
        DbContext = context;
    }

    //THRESHOLD PRESETS
    public async Task<IQueryable<CompanyThresholdPreset>> GetCompanyThresholdPresetsByCompanyId(long companyId) =>
        await Task.Run(() =>
            DbContext.CompanyThresholdPreset.Where(x => x.CompanyId == companyId));
}