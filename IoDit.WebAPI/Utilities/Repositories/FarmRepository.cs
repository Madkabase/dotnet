namespace IoDit.WebAPI.Utilities.Repositories;

using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities.Company;
using Microsoft.EntityFrameworkCore;

public class FarmRepository : IFarmRepository
{
    public IoDitDbContext DbContext { get; }

    public FarmRepository(IoDitDbContext context)
    {
        DbContext = context;
    }


    public async Task<IQueryable<CompanyFarm>> GetCompanyFarms(long companyId) =>
        await Task.Run(() =>
            DbContext.Companies.Include(x => x.Farms).Where(x => x.Id == companyId).SelectMany(x => x.Farms));

    public async Task<CompanyUser?> GetCompanyUserFarms(long companyUserId) =>
        await Task.Run(() => DbContext.CompanyUsers
            .Include(x => x.CompanyFarmUsers).ThenInclude(x => x.CompanyFarm)
            .FirstOrDefault(x => x.Id == companyUserId));

    public async Task<CompanyFarm?> GetCompanyFarmById(long companyFarmId) =>
        await Task.Run(() => DbContext.CompanyFarms.FirstOrDefault(x => x.Id == companyFarmId));


}