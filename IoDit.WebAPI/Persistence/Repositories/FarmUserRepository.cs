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

    public async Task<FarmUser> AddFarmUser(FarmUser farmUser)
    {
        var user = _context.FarmUsers.Add(farmUser).Entity;
        await _context.SaveChangesAsync();
        return user!;
    }
}
