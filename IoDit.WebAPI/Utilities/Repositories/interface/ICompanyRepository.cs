using IoDit.WebAPI.Persistence.Entities.Company;

namespace IoDit.WebAPI.Utilities.Repositories;
public interface ICompanyRepository
{
    //COMPANIES
    Task<Company?> GetCompanyById(long companyId);
    Task<IQueryable<Company>> GetCompanies();
    Task<IQueryable<SubscriptionRequest>> GetSubscriptionRequests();
}