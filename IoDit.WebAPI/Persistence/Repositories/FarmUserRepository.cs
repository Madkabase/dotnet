using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class FarmUserRepository : IFarmUserRepository
{
    private readonly AgroditDbContext _context;
    public FarmUserRepository(AgroditDbContext context)
    {
        _context = context;
    }

    public async Task<List<FarmUser>> getUserFarms(User user) =>
         await Task.Run(() => _context.FarmUsers.Include(fu => fu.Farm).Where(fu => fu.User.Id == user.Id).ToList());

    public async Task<FarmUser?> GetUserFarm(long farmId, long userId) =>
        await Task.Run(() => _context.FarmUsers.Include(fu => fu.Farm).Include(fu => fu.User).FirstOrDefaultAsync(fu => fu.Farm.Id == farmId && fu.User.Id == userId));

    public async Task<FarmUser> AddFarmUser(FarmUser farmUser) =>
        await Task.Run(() => _context.FarmUsers.Add(farmUser).Entity);

    public async Task<List<User>> GetUsersNotFromFarmByQuery(int farmId, string? search) =>

        await Task.Run(() => search == null ? new List<User>() : _context.Users
        .FromSqlRaw(
                    "SELECT * FROM \"Users\" WHERE \"Id\" NOT IN (SELECT \"UserId\" FROM \"FarmUsers\" WHERE \"FarmId\" = {0}) AND (\"FirstName\" LIKE {1} OR \"LastName\" LIKE {1} OR \"Email\" LIKE {1})",
                    farmId,
                    $"%{search}%"
                   )
        .ToList());
}
