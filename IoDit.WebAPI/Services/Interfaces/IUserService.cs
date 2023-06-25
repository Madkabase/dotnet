using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public interface IUserService
{
    /// <summary>
    ///     Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user to be created.</param>
    /// <returns>The created user.</returns>
    public Task<User?> GetUserByEmail(string email);
}