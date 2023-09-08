using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
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


    public async Task<UserBo> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmail(email)
            ?? throw new EntityNotFoundException("User not found");
        return UserBo.FromEntity(user);

    }
}