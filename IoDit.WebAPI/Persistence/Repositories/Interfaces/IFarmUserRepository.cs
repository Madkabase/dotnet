using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IFarmUserRepository
{
    public Task<List<FarmUser>> getUserFarms(UserBo user);
    public Task<FarmUser?> GetUserFarm(long farmId, long userId);
    public Task<FarmUser> AddFarmUser(FarmUserBo farmUser);
}