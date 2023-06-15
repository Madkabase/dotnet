namespace IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using IoDit.WebAPI.Persistence.Entities.Company;



public class CompanyRepository : ICompanyRepository
{

    public IoDitDbContext DbContext { get; }

    public CompanyRepository(IoDitDbContext context)
    {
        DbContext = context;
    }

    //COMPANY
    public async Task<Company?> GetCompanyById(long companyId) =>
        await Task.Run(() => DbContext.Companies.Include(x => x.Owner).FirstOrDefault(x => x.Id == companyId));

    public async Task<IQueryable<Company>> GetCompanies() =>
        await Task.Run(() => DbContext.Companies.Include(x => x.Owner));

    public async Task<IQueryable<SubscriptionRequest>> GetSubscriptionRequests() =>
        await Task.Run(() => DbContext.SubscriptionRequests);

}