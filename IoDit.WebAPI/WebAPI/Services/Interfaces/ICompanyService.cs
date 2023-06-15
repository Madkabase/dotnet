using IoDit.WebAPI.WebAPI.Models.Company;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface ICompanyService
{
    Task<GetCompanyResponseDto> CreateCompany(CreateCompanyRequestDto request);
    Task<GetCompanyResponseDto?> GetCompany(long companyId);
    Task<List<GetCompanyResponseDto>?> GetCompanies();
    Task<List<SubscriptionRequestResponseDto>?> GetSubscriptionRequests();
    Task<SubscriptionRequestResponseDto?> CreateSubscriptionRequest(CreateRequestSubscriptionRequestDto request);
}