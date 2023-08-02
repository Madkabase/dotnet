using System.Security.Cryptography;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;

namespace IoDit.WebAPI.Services;

public class RefreshJwtService : IRefreshJwtService
{

    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUtilsRepository _utilsRepository;
    private readonly IUserService _userService;

    public RefreshJwtService(
        IConfiguration configuration,
        IRefreshTokenRepository refreshTokenRepository,
        IUtilsRepository utilsRepository,
        IUserService userService
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
    public async Task<RefreshTokenBo> GenerateRefreshToken(UserBo user, string deviceIdentifier)
    {
        List<RefreshTokenBo> userTokens = (await _refreshTokenRepository.GetRefreshTokensForUser(user)).Select(x => new RefreshTokenBo
        {
            DeviceIdentifier = x.DeviceIdentifier,
            Expires = x.Expires,
            Id = x.Id,
            Token = x.Token,
            User = new UserBo
            {
                Id = x.User.Id,
            }
        }).ToList();

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
            await _refreshTokenRepository.UpdateToken(currentToken);
            return currentToken;
        }

        var refreshToken = new RefreshTokenBo()
        {
            Token = newToken,
            Expires = DateTime.UtcNow.AddDays(7),
            DeviceIdentifier = deviceIdentifier,
            User = user

        };

        await _refreshTokenRepository.CreateRefreshToken(refreshToken);
        return refreshToken;
    }

    public async Task<RefreshTokenBo> GetRefreshTokenByToken(String token)
    {
        var refreshToken = await _refreshTokenRepository.GetRefreshTokenByToken(token);
        if (refreshToken == null)
        {
            throw new EntityNotFoundException("Refresh token not found");
        }
        return RefreshTokenBo.FromEntity(refreshToken);

    }

    public async Task<List<RefreshTokenBo>> GetRefreshTokensForUser(UserBo user)
    {
        return (await _refreshTokenRepository.GetRefreshTokensForUser(user))
                    .Select(x => RefreshTokenBo.FromEntity(x)).ToList();
    }


    public bool isExpired(RefreshTokenBo refreshToken)
    {
        return refreshToken.Expires < DateTime.UtcNow;
    }
    /// <summary>
    /// Deletes the expired refresh tokens in the given list 
    /// </summary>
    /// <param name="userTokens">The list of refresh tokens to check</param>
    private async Task CleanExpiredTokens(List<RefreshTokenBo> userTokens)
    {
        var expiredTokens = userTokens.Where(t => t.Expires < DateTime.UtcNow).ToList();
        if (expiredTokens.Any())
        {
            await _utilsRepository.DeleteRangeAsync(expiredTokens.Select(x => RefreshToken.FromBo(x)).ToList());
        }
    }

}