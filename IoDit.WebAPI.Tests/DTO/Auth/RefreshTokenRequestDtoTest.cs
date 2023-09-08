using IoDit.WebAPI.DTO.Auth;

namespace IoDit.WebAPI.Tests.DTO;

public class RefreshTokenRequestDtoTest
{
    [Fact]
    public void should_be_wellFormed()
    {
        // Arrange
        var refreshTokenRequestDto = new RefreshTokenRequestDto();

        // Act
        refreshTokenRequestDto.DeviceIdentifier = "test";
        refreshTokenRequestDto.RefreshToken = "test";
        // Assert

        Assert.Equal("test", refreshTokenRequestDto.DeviceIdentifier);
        Assert.Equal("test", refreshTokenRequestDto.RefreshToken);
    }
}
