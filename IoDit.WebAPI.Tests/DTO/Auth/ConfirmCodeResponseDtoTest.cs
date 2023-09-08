using IoDit.WebAPI.Models.Auth;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Tests.Models.Auth;

public class ConfirmCodeResponseDtoTest
{
    [Fact]
    public void ShouldCreateConfirmCodeResponseDto()
    {

        // Arrange
        var confirmCodeResponseDto = new ConfirmCodeResponseDto
        {
            CodeConfirmationFlowType = ConfirmCodeFlowType.InvalidCode,
            Message = "Invalid code"
        };

        // Act

        // Assert

        Assert.Equal(ConfirmCodeFlowType.InvalidCode, confirmCodeResponseDto.CodeConfirmationFlowType);
        Assert.Equal("Invalid code", confirmCodeResponseDto.Message);

    }
}