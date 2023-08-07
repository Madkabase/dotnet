using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IFarmUserRepository
{
    public Task<List<FarmUser>> GetUserFarms(UserBo user);
    public Task<List<FarmUser>> GetFarmUsers(FarmBo farm);
    public Task<FarmUser?> GetUserFarm(long farmId, long userId);
    public Task<FarmUser?> AddFarmUser(FarmUserBo farmUser);
    Task RemoveFarmUser(FarmUserBo farmUser);
}