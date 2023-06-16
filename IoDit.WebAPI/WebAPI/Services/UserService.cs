using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.User;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICompanyService _companyService;
    public UserService(IUserRepository repository,
        ICompanyService companyService)
    {
        _userRepository = repository;
        _companyService = companyService;
    }

    public async Task<UserResponseDto?> GetUser(string userEmail)
    {
        var user = await _userRepository.GetUserByEmail(userEmail);
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
            LastName = user.LastName,
            Companies = await _companyService.GetCompaniesByUserId(user.Id)
        };
    }
}