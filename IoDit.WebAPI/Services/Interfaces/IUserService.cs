using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public interface IUserService
{
    /// <summary>
    ///     Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user to be created.</param>
    /// <returns>The created user.</returns>
    /// <exception cref="EntityNotFoundException">Thrown when the user is not found.</exception>s
    public Task<UserBo> GetUserByEmail(string email);
}