using IoDit.WebAPI.DTO.Auth;

namespace IoDit.WebAPI.Tests.DTO.Auth;

public class LoginRequestDtoTest
{
    private const string EmailAddress = "email@address.com";
    private const string Password = "password";

    [Fact]
    public void ShouldCreateLoginRequestDto()
    {
        // Arrange
        var loginRequestDto = new LoginRequestDto
        {
            Email = EmailAddress,
            Password = Password
        };

        // Act

        // Assert
        Assert.Equal(EmailAddress, loginRequestDto.Email);
        Assert.Equal(Password, loginRequestDto.Password);
    }
}