using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IFarmUserRepository
{
    public Task<List<FarmUser>> getUserFarms(User user);
    public Task<FarmUser?> GetUserFarm(long farmId, long userId);
    public Task<FarmUser> AddFarmUser(FarmUser farmUser);
    Task<List<User>> GetUsersNotFromFarmByQuery(int farmId, string? search);
}