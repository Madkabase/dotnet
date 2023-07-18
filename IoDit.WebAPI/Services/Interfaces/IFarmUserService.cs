using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Services;

public interface IFarmUserService
{

    public Task<List<UserFarmDto>> getUserFarms(UserDto user);
    public Task<FarmUser?> GetUserFarm(long farmId, long userId);
    public Task<bool> HasAccessToFarm(Farm farm, User user);
    Task<FarmUser> AddFarmer(Farm farm, User userToAdd, FarmRoles role);
}