using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.WebAPI.Models.Company;

namespace IoDit.WebAPI.Utilities.Repositories;
public interface ICompanyRepository
{
    //COMPANIES
    Task<Company?> GetCompanyById(long companyId);
    Task<IQueryable<Company>> GetCompanies();
    Task<IQueryable<SubscriptionRequest>> GetSubscriptionRequests();
    Task<IQueryable<Company>> GetCompaniesByUserId(long userId);
}