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

    /// <summary>
    /// get user by id
    /// </summary>
    /// <param name="userId"> the id of the user </param>
    /// <returns>the user with the given id</returns>
    public async Task<User?> GetUserById(long userId) =>
        await Task.Run(() => DbContext.Users.FirstOrDefault(x => x.Id == userId));

    /// <summary>
    /// get user by email
    /// </summary>
    /// <param name="email"> the email of the user </param>
    /// <returns>the user with the given email</returns>
    public async Task<User?> GetUserByEmail(string email) =>
        await Task.Run(() => DbContext.Users.FirstOrDefault(x => x.Email == email));

    /// <summary>
    /// get Refresh Token from 
    /// </summary>
    /// <param name="token"> the token of the user </param>
    public async Task<RefreshToken?> GetRefreshToken(string token) =>
        await Task.Run(() => DbContext.RefreshTokens.FirstOrDefault(x => x.Token == token));

    /// <summary>
    /// checks if the refresh token exist
    /// </summary>
    /// <param name="token"> the token of the user </param>
    /// <returns>true if the token exists</returns>
    public async Task<bool> CheckIfRefreshTokenExist(string token) =>
        await Task.Run(() => DbContext.RefreshTokens.FirstOrDefault(x => x.Token == token) == null);

    /// <summary>
    /// get the refresh tokens for a user
    /// </summary>
    /// <param name="userId"> the id of the user </param>
    /// <returns>the refresh token for the user</returns>
    public async Task<IQueryable<RefreshToken>> GetRefreshTokensForUser(long userId) =>
        await Task.Run(() => DbContext.RefreshTokens.Select(x => x).Where(x => x.UserId == userId));

}