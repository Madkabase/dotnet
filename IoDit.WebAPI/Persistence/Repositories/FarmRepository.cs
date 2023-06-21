using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class FarmRepository
{
    private readonly AgroditDbContext _context;
    public FarmRepository(AgroditDbContext context)
    {
        _context = context;
    }

    internal Task<List<FarmUser>> getUserFarms(User user) =>
    Task.Run(() => _context.FarmUsers.Include(fu => fu.Farm).Where(fu => fu.User.Id == user.Id).ToList());

}