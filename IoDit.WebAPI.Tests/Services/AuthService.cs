using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Utilities.Helpers;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.Extensions.Configuration;
using Moq;

namespace IoDit.WebAPI.Tests.Services;

public class AuthServiceTest
{

    [Fact]
    public async void ShouldLogin()
    {
        // Arrange
        var user = new UserBo
        {
            Id = 1,
            Email = "email@test.com",
            Password = "test",
            AppRole = AppRoles.AppUser,
            IsVerified = true,

        };

        var userPassword = user.Password;

        user.Password = PasswordEncoder.HashPassword(user.Password);



        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(user);

        var refreshTokenService = new Mock<IRefreshJwtService>();
        refreshTokenService.Setup(x => x.GenerateRefreshToken(It.IsAny<UserBo>(), It.IsAny<string>())).ReturnsAsync(
            new RefreshTokenBo
            {
                Token = "refreshToken",
                DeviceIdentifier = "deviceId",
                Expires = DateTime.Now.AddDays(1)
            });

        var mockJwtHelper = new Mock<IJwtHelper>();
        mockJwtHelper.Setup(x => x.GenerateJwtToken(It.IsAny<string>())).Returns("token");

        var emailSerivce = new Mock<IEmailHelper>();

        var utilsRepository = new Mock<IUtilsRepository>();

        var farmUserService = new Mock<IFarmUserService>();
        var _configuration = new Mock<IConfiguration>();

        var authService = new AuthService(mockUserService.Object, refreshTokenService.Object, mockJwtHelper.Object, emailSerivce.Object, utilsRepository.Object, _configuration.Object);

        // Act
        var result = await authService.Login(email: user.Email, password: userPassword, DeviceId: "deviceId");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("token", result.Token);
    }
}
