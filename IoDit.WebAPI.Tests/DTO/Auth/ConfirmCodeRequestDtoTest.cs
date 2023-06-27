using IoDit.WebAPI.DTO.Auth;

namespace IoDit.WebAPI.Tests.DTO.Auth;

public class ConfirmCodeRequestDtoTest
{
    [Fact]
    public void ShouldCreateConfirmCodeRequestDto()
    {
        // Arrange
        var confirmCodeRequestDto = new ConfirmCodeRequestDto
        {
            Email = "email@address.com",
            Code = 123456
        };

        // Act

        // Assert

        Assert.Equal("email@address.com", confirmCodeRequestDto.Email);
        Assert.Equal(123456, confirmCodeRequestDto.Code);
    }
}