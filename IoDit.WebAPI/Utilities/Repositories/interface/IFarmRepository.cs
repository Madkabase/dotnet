namespace IoDit.WebAPI.Utilities.Repositories;

using IoDit.WebAPI.Persistence.Entities.Company;

public interface IFarmRepository
{
    //FARM
    Task<IQueryable<CompanyFarm>> GetCompanyFarms(long companyId);
    Task<CompanyUser?> GetCompanyUserFarms(long companyUserId);
    Task<CompanyFarm?> GetCompanyFarmById(long companyFarmId);

}