
using IoDit.WebAPI.DTO.Auth;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Tests.DTO.Auth;

public class LoginResponseDtoTest
{
    public static string email = "email@address.com";
    public static UserDto user = new UserDto
    {
        Id = 1,
        Email = email,
        FirstName = "First",
        LastName = "Last",
        AppRole = AppRoles.AppUser,
    };

    [Fact]
    public void ShouldCreateLoginResponseDto()
    {
        // Arrange
        var loginResponseDto = new LoginResponseDto
        {
            User = user,
            RefreshToken = "refreshToken",
            Token = "token"
        };

        // Act

        // Assert

        Assert.Equal(user, loginResponseDto.User);
        Assert.Equal("refreshToken", loginResponseDto.RefreshToken);
        Assert.Equal("token", loginResponseDto.Token);

    }
}