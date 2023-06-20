using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class FarmUserRepository
{
    private readonly AgroditDbContext _context;
    public FarmUserRepository(AgroditDbContext context)
    {
        _context = context;
    }

    internal async Task<List<FarmUser>> getUserFarms(User user) =>
         await Task.Run(() => _context.FarmUsers.Include(fu => fu.Farm).Where(fu => fu.User.Id == user.Id).ToList());
}