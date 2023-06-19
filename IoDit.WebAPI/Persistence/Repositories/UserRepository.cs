

using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public class UserRepository
{
    public AgroditDbContext DbContext { get; }

    public UserRepository(AgroditDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<User?> GetUserByEmail(String email)
    {
        return await Task.Run(() => DbContext.Users.FirstOrDefault(u => u.Email == email));
    }

    public async Task<User?> GetUserById(long id)
    {
        return await Task.Run(() => DbContext.Users.FirstOrDefault(u => u.Id == id));
    }
}