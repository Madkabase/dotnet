using IoDit.WebAPI.Persistence.Entities.Company;

namespace IoDit.WebAPI.Utilities.Repositories;

public interface ICompanyFarmUserRepository
{
    Task<IQueryable<CompanyFarmUser>> GetUsersOfCompanyByCompanyId(long companyId);
    Task<IQueryable<CompanyFarmUser>> GetCompanyUserFarmUsers(long companyUserId);
    Task<CompanyFarmUser?> GetCompanyUserFarmUser(long companyFarmId, long companyUserId);
}