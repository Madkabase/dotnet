namespace IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Persistence.Entities.Company;

public interface IFieldRepository
{
    Task<IQueryable<CompanyField>> GetFieldsByCompanyId(long companyId);
    Task<CompanyField?> GetFieldById(long companyFieldId);
}