using IoDit.WebAPI.WebAPI.Models.Farm;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface IFarmService
{
    Task<CompanyFarmsResponseDto> CreateCompanyFarm(CreateCompanyFarm request);
    Task<List<CompanyFarmsResponseDto>?> GetCompanyFarms(long companyId);
}