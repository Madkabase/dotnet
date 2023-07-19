using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Services;

public interface IFarmUserService
{
    /// <summary>
    /// Gets the farms for the user
    /// </summary>
    /// <param name="user">user object</param>
    /// <returns>list of farms the user is part of</returns>
    public Task<List<UserFarmDto>> getUserFarms(UserDto user);
    /// <summary>
    /// Gets the farmuser object for the user
    /// </summary>
    /// <param name="farmId">farm id</param>
    /// <param name="userId">user id</param>
    /// <returns>the farm for the user if user is part of the farm</returns>
    public Task<FarmUser?> GetUserFarm(long farmId, long userId);
    /// <summary>
    /// Checks if the user has access to the farm
    /// </summary>
    /// <param name="farm">farm needed to be checked</param>
    /// <param name="user">user trying to access the farm</param>
    /// <returns>a boolean that confirms the access or not</returns> 
    public Task<bool> HasAccessToFarm(Farm farm, User user);
    /// <summary>
    /// Adds a user to a farm
    /// </summary>
    /// <param name="farm">farm to add the user to</param>
    /// <param name="userToAdd">user to add to the farm</param>
    /// <param name="role">role of the user in the farm</param>
    /// <returns>the farmuser object for the user</returns>
    Task<FarmUser> AddFarmer(Farm farm, User userToAdd, FarmRoles role);
}