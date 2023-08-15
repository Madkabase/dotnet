using IoDit.WebAPI.BO;
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

    public async Task<List<FarmUser>> GetUserFarms(UserBo user) =>
         await Task.Run(() => _context.FarmUsers.Include(fu => fu.Farm).Where(fu => fu.User.Id == user.Id).ToList());

    public async Task<List<FarmUser>> GetFarmUsers(FarmBo farm) =>
        await Task.Run(() => _context.FarmUsers.Include(fu => fu.User).Where(fu => fu.Farm.Id == farm.Id).ToList());
    public async Task<FarmUser?> GetUserFarm(long farmId, long userId) =>
        await Task.Run(() => _context.FarmUsers.Include(fu => fu.Farm).Include(fu => fu.User).FirstOrDefaultAsync(fu => fu.Farm.Id == farmId && fu.User.Id == userId));

    public async Task<FarmUser?> AddFarmUser(FarmUserBo farmUser)
    {
        FarmUser farmUserE = new()
        {

            FarmRole = farmUser.FarmRole,
            FarmId = farmUser.Farm.Id,
            UserId = farmUser.User.Id,
        };
        var user = _context.FarmUsers.Add(farmUserE).Entity;

        await _context.SaveChangesAsync();
        return user;
    }

    public Task RemoveFarmUser(FarmUserBo farmUser) => Task.Run(() =>
       {
           _context.FarmUsers.Remove(FarmUser.FromBo(farmUser));
           _context.SaveChanges();
       }
    );

    public Task<List<FarmUser>> GetFarmAdmins(long id)
    {
        return _context.FarmUsers
            .Include(fu => fu.User)
            .Where(fu => fu.Farm.Id == id && fu.FarmRole == Utilities.Types.FarmRoles.Admin)
            .ToListAsync();
    }
}
