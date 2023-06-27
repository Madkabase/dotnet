using IoDit.WebAPI.Utilities.Types;

using IoDit.WebAPI.DTO.Auth;
namespace IoDit.WebAPI.DTO.Auth;

public class RegisterResponseDtoTest
{

    [Fact]
    public void ShouldCreateRegisterResponseDto()
    {
        // Arrange
        var registerResponseDto = new RegisterResponseDto
        {
            Message = "message",
            RegistrationFlowType = RegistrationFlowType.NewUser
        };

        // Act

        // Assert
        Assert.Equal("message", registerResponseDto.Message);
        Assert.Equal(RegistrationFlowType.NewUser, registerResponseDto.RegistrationFlowType);
    }
}