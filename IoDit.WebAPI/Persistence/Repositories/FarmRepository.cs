using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class FarmRepository : IFarmRepository
{
    private readonly AgroditDbContext _context;
    public FarmRepository(AgroditDbContext context)
    {
        _context = context;
    }

    public Task<List<FarmUser>> getUserFarms(User user) =>
    Task.Run(() => _context.FarmUsers.Include(fu => fu.Farm).ThenInclude(f => f.Owner).Where(fu => fu.User.Id == user.Id).ToList());

    public Task<Farm?> getFarmDetailsById(long farmId) =>
    Task.Run(() => _context.Farms
        .Include(f => f.Owner)
        .Include(f => f.FarmUsers)
        .ThenInclude(fu => fu.User)
        .Include(f => f.Fields)
        .FirstOrDefault(f => f.Id == farmId)
    );
}