using IoDit.WebAPI.WebAPI.Models.CompanyUser;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface ICompanyUserService
{
    Task<List<GetCompanyUsersResponseDto>?> GetUserCompanyUsers(string email);
    Task<List<GetCompanyUsersResponseDto>?> GetCompanyUsers(long companyId);
    Task<GetCompanyUsersResponseDto?> InviteUser(InviteUserRequestDto request);
}