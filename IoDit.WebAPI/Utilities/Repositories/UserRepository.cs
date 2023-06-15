using IoDit.WebAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using IoDit.WebAPI.Persistence.Entities;
namespace IoDit.WebAPI.Utilities.Repositories;


public class UserRepository : IUserRepository
{
    public IoDitDbContext DbContext { get; }

    public UserRepository(IoDitDbContext dbContext)
    {
        DbContext = dbContext;
    }

    //USERS
    public async Task<IQueryable<User>> GetUsers() => await Task.Run(() => DbContext.Users);

    public async Task<User?> GetUserById(long userId) =>
        await Task.Run(() => DbContext.Users.FirstOrDefault(x => x.Id == userId));

    // get user by email
    public async Task<User?> GetUserByEmail(string email) =>
        await Task.Run(() => DbContext.Users.FirstOrDefault(x => x.Email == email));

    public async Task<Boolean> IsUserFarmField(string email, long fieldId) => await Task.Run(
        () =>
        DbContext
          .Companies
          .Include(x => x.Users)
          .Include(x => x.Fields)
          .Any(x => x.Users.Any(x => x.User.Email == email) && x.Fields.Any(x => x.Id == fieldId))
    );

    public async Task<RefreshToken?> GetRefreshToken(string token) =>
        await Task.Run(() => DbContext.RefreshTokens.FirstOrDefault(x => x.Token == token));

    public async Task<bool> CheckIfRefreshTokenExist(string token) =>
        await Task.Run(() => DbContext.RefreshTokens.FirstOrDefault(x => x.Token == token) == null);

    public async Task<IQueryable<RefreshToken>> GetRefreshTokensForUser(long userId) =>
        await Task.Run(() => DbContext.RefreshTokens.Select(x => x).Where(x => x.UserId == userId));

}