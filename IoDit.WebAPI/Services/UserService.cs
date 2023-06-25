using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    ///     Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user to be created.</param>
    /// <returns>The created user.</returns>
    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user == null)
        {
            return null;
        }
        return user;
    }
}