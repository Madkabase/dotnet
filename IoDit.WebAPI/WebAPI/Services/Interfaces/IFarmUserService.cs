using IoDit.WebAPI.WebAPI.Models.FarmUser;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface IFarmUserService
{
    Task<List<GetFarmUsersResponseDto>?> GetUserFarmUsers(long companyUserId);
    Task<List<GetFarmUsersResponseDto>?> GetFarmUsers(long companyId);
    Task<GetFarmUsersResponseDto?> AssignUserToFarm(AssignUserToFarmRequestDto request);
}