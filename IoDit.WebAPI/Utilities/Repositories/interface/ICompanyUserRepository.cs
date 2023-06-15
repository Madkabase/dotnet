using IoDit.WebAPI.Persistence.Entities.Company;

namespace IoDit.WebAPI.Utilities.Repositories;

public interface ICompanyUserRepository
{
    //COMPANY USERS
    Task<IQueryable<CompanyUser>> GetCompanyUsers(long companyId);
    Task<IQueryable<CompanyUser>> GetUserCompanyUsers(string email);
    Task<bool> CheckIfUserCanAccessCompanyUser(string email, long companyUserId);
    Task<CompanyUser?> GetCompanyUserForUserSecure(string email, long companyUserId);
    Task<IQueryable<CompanyUser>> GetCompanyAdmins(long companyId);
    Task<CompanyUser?> GetCompanyUserForUserByCompanyId(string email, long companyId);
    Task<CompanyUser?> GetCompanyUserById(long companyUserId);

    //USER THRESHOLDS
    Task<IQueryable<CompanyUserDeviceData>> GetCompanyUserThresholds(long companyUserId);

}