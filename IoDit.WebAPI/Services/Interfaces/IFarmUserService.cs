using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public interface IFarmUserService
{

    public Task<List<UserFarmDto>> getUserFarms(UserDto user);
    public Task<UserFarmDto?> GetUserFarm(long farmId, long userId);

}