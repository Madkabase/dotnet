using System.Security.Cryptography;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class RefreshJwtService
{

    private readonly IConfiguration _configuration;
    private readonly RefreshTokenRepository _refreshTokenRepository;
    private readonly UtilsRepository _utilsRepository;
    private readonly UserService _userService;

    public RefreshJwtService(
        IConfiguration configuration,
        RefreshTokenRepository refreshTokenRepository,
        UtilsRepository utilsRepository,
        UserService userService
        )
    {
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
        _utilsRepository = utilsRepository;
        _userService = userService;
    }

    public async Task<RefreshToken> GenerateRefreshToken(User user, string deviceIdentifier)
    {

        var userTokens = await _refreshTokenRepository.GetRefreshTokensForUser(user);
        var expiredTokens = userTokens.Where(t => t.Expires < DateTime.UtcNow).ToList();
        if (expiredTokens.Any())
        {
            await _utilsRepository.DeleteRangeAsync(expiredTokens);
        }
        var currentToken = userTokens.FirstOrDefault(x => x.DeviceIdentifier == deviceIdentifier);

        bool isTokenUnique;
        string newToken;

        do
        {
            newToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            isTokenUnique = await _refreshTokenRepository.DoesRefreshTokenExist(newToken);
        }
        while (!isTokenUnique);

        if (currentToken != null)
        {
            currentToken.Token = newToken;
            currentToken.Expires = DateTime.UtcNow.AddDays(7);
            await _utilsRepository.UpdateAsync(currentToken);
            return currentToken;
        }

        var refreshToken = new RefreshToken()
        {
            Token = newToken,
            Expires = DateTime.UtcNow.AddDays(7),
            DeviceIdentifier = deviceIdentifier,
            User = user,
        };

        await _utilsRepository.CreateAsync(refreshToken);
        return refreshToken;
    }
}