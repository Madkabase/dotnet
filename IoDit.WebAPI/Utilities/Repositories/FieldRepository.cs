using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Utilities.Repositories;

public class FieldRepository : IFieldRepository
{

    public IoDitDbContext DbContext { get; }

    public FieldRepository(IoDitDbContext context)
    {
        DbContext = context;
    }

    public async Task<IQueryable<CompanyField>> GetFieldsByCompanyId(long companyId) =>
        await Task.Run(() =>
            DbContext.Companies.Include(x => x.Fields).Where(x => x.Id == companyId).SelectMany(x => x.Fields));

    public async Task<CompanyField?> GetFieldById(long companyFieldId) =>
        await Task.Run(() =>
            DbContext.CompanyFields.FirstOrDefault(x => x.Id == companyFieldId));


}