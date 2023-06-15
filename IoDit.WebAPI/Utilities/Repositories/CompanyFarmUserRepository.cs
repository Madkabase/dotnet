using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Utilities.Repositories;

class CompanyFarmUserRepository : ICompanyFarmUserRepository
{

    public IoDitDbContext DbContext { get; }

    public CompanyFarmUserRepository(IoDitDbContext context)
    {
        DbContext = context;
    }

    public async Task<IQueryable<CompanyFarmUser>> GetUsersOfCompanyByCompanyId(long companyId) =>
        await Task.Run(() =>
            DbContext.Companies.Include(x => x.FarmUsers).Where(x => x.Id == companyId).SelectMany(x => x.FarmUsers));

    public async Task<IQueryable<CompanyFarmUser>> GetCompanyUserFarmUsers(long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyFarmUsers.Where(x => x.CompanyUserId == companyUserId));

    public async Task<CompanyFarmUser?> GetCompanyUserFarmUser(long companyFarmId, long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyFarmUsers.FirstOrDefault(x =>
                x.CompanyUserId == companyUserId && x.CompanyFarmId == companyFarmId));

}