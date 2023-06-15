namespace IoDit.WebAPI.Utilities.Repositories;

using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.EntityFrameworkCore;

public class CompanyUserRepository : ICompanyUserRepository
{

    public IoDitDbContext DbContext { get; }

    public CompanyUserRepository(IoDitDbContext context)
    {
        DbContext = context;
    }

    public async Task<IQueryable<CompanyUser>> GetCompanyUsers(long companyId) =>
        await Task.Run(() =>
            DbContext.Companies.Include(x => x.Users).Where(x => x.Id == companyId).SelectMany(x => x.Users));

    public async Task<IQueryable<CompanyUser>> GetUserCompanyUsers(string email) =>
        await Task.Run(() =>
            DbContext.CompanyUsers.Include(x => x.Company).Include(x => x.User).Select(x => x)
                .Where(x => x.User.Email == email));

    public async Task<CompanyUser?> GetCompanyUserForUserSecure(string email, long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyUsers.Include(x => x.User)
                .FirstOrDefault(x => x.User.Email == email && x.Id == companyUserId));

    public async Task<IQueryable<CompanyUser>> GetCompanyAdmins(long companyId) =>
        await Task.Run(() =>
            DbContext.CompanyUsers.Select(x => x)
                .Where(x => x.CompanyId == companyId && x.CompanyRole != CompanyRoles.CompanyUser));

    public async Task<bool> CheckIfUserCanAccessCompanyUser(string email, long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyUsers.Include(x => x.User)
                .FirstOrDefault(x => x.User.Email == email && x.Id == companyUserId) != null);

    public async Task<CompanyUser?> GetCompanyUserForUserByCompanyId(string email, long companyId) =>
        await Task.Run(() =>
            DbContext.CompanyUsers.FirstOrDefault(x => x.User.Email == email && x.CompanyId == companyId));

    public async Task<CompanyUser?> GetCompanyUserById(long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyUsers.FirstOrDefault(x => x.Id == companyUserId));

}