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
}