
using IoDit.WebAPI.DTO.Auth;
namespace IoDit.WebAPI.Tests.DTO.Auth;

public class RegisterRequestDtoTest
{
    public static string email = "email@address.com";
    public static string password = "password";
    public static string confirmPassword = "password";
    public static string firstName = "First";
    public static string lastName = "Last";

    [Fact]
    public void ShouldCreateRegisterRequestDto()
    {
        // Arrange
        var registerRequestDto = new RegisterRequestDto
        {
            Email = email,
            Password = password,
            ConfirmPassword = confirmPassword,
            FirstName = firstName,
            LastName = lastName
        };

        // Act

        // Assert
        Assert.Equal(email, registerRequestDto.Email);
        Assert.Equal(password, registerRequestDto.Password);
        Assert.Equal(confirmPassword, registerRequestDto.ConfirmPassword);
        Assert.Equal(firstName, registerRequestDto.FirstName);
        Assert.Equal(lastName, registerRequestDto.LastName);
    }
}