using IoDit.WebAPI.BO;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Services;

public interface IFarmUserService
{
    /// <summary>
    /// Gets the farms for the user
    /// </summary>
    /// <param name="user">user object</param>
    /// <returns>list of farms the user is part of</returns>
    public Task<List<FarmUserBo>> GetUserFarms(UserBo user);
    /// <summary>
    /// Gets the farmuser object for the user
    /// </summary>
    /// <param name="farmId">farm id</param>
    /// <param name="userId">user id</param>
    /// <returns>the farm for the user if user is part of the farm</returns>
    public Task<FarmUserBo> GetUserFarm(long farmId, long userId);
    /// <summary>
    /// Gets the users for the farm
    /// </summary>
    /// <param name="farm"></param>
    /// <returns></returns>
    public Task<List<FarmUserBo>> GetFarmUsers(FarmBo farm);
    /// <summary>
    /// Checks if the user has access to the farm
    /// </summary>
    /// <param name="farm">farm needed to be checked</param>
    /// <param name="user">user trying to access the farm</param>
    /// <returns>a boolean that confirms the access or not</returns> 
    public Task<bool> HasAccessToFarm(FarmBo farm, UserBo user);
    /// <summary>
    /// Adds a user to a farm
    /// </summary>
    /// <param name="farm">farm to add the user to</param>
    /// <param name="userToAdd">user to add to the farm</param>
    /// <param name="role">role of the user in the farm</param>
    /// <returns>the farmuser object for the user</returns>
    Task<FarmUserBo> AddFarmer(FarmBo farm, UserBo userToAdd, FarmRoles role);
    Task RemoveFarmer(FarmUserBo farmUser);
}