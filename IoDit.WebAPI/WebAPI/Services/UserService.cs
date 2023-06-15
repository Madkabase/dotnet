using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.User;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class UserService : IUserService
{
    private readonly IIoDitRepository _repository;

    public UserService(IIoDitRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserResponseDto?> GetUser(string userEmail)
    {
        var user = await _repository.GetUserByEmail(userEmail);
        if (user == null)
        {
            return null;
        }
        return new UserResponseDto()
        {
            Email = user.Email,
            Id = user.Id,
            AppRole = user.AppRole,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}