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

    /// <summary>
    /// Generates a new refresh token for a user
    /// </summary>
    /// <param name="user">The user to generate the token for</param>
    /// <param name="deviceIdentifier">The device identifier of the user</param>
    /// <returns>A refresh token</returns>
    public async Task<RefreshToken> GenerateRefreshToken(User user, string deviceIdentifier)
    {
        var userTokens = await _refreshTokenRepository.GetRefreshTokensForUser(user);

        await CleanExpiredTokens(userTokens);

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

    public async Task<RefreshToken?> GetRefreshTokenByToken(String token)
    {
        return await _refreshTokenRepository.GetRefreshTokenByToken(token);
    }

    public async Task<bool> isExpired(RefreshToken refreshToken)
    {
        return refreshToken.Expires < DateTime.UtcNow;
    }
    /// <summary>
    /// Deletes the expired refresh tokens in the given list 
    /// </summary>
    /// <param name="userTokens">The list of refresh tokens to check</param>
    private async Task CleanExpiredTokens(List<RefreshToken> userTokens)
    {
        var expiredTokens = userTokens.Where(t => t.Expires < DateTime.UtcNow).ToList();
        if (expiredTokens.Any())
        {
            await _utilsRepository.DeleteRangeAsync(expiredTokens);
        }
    }

}