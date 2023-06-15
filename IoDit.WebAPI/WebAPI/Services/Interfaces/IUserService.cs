using IoDit.WebAPI.WebAPI.Models.User;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface IUserService
{
    Task<UserResponseDto?> GetUser(string userEmail);
}