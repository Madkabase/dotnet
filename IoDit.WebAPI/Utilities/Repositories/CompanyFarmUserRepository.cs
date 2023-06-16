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

    /// <summary>
    /// get the users of the company
    /// </summary>
    /// <param name="companyId"> the id of the company </param>
    /// <param name="companyFarmId"> the id of the company farm </param>
    /// <returns>The users of the company</returns>
    public async Task<IQueryable<CompanyFarmUser>> GetUsersOfCompanyByCompanyId(long companyId) =>
        await Task.Run(() =>
            DbContext.Companies.Include(x => x.FarmUsers).Where(x => x.Id == companyId).SelectMany(x => x.FarmUsers));

    /// <summary>
    /// get the company with the given id if it includes the isuer
    /// </summary>
    /// <param name="companyId"> the id of the company </param>
    /// <param name="userId"> the id of the user </param>
    /// <returns>the company with the given id if it includes the isuer</returns>
    public async Task<CompanyFarmUser?> GetCompanyUserFarmUser(long companyFarmId, long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyFarmUsers.FirstOrDefault(x =>
                x.CompanyUserId == companyUserId && x.CompanyFarmId == companyFarmId));

}