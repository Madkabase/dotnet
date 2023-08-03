using System.Security.Cryptography;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public interface IRefreshJwtService
{

    /// <summary>
    /// Generates a new refresh token for a user
    /// </summary>
    /// <param name="user">The user to generate the token for</param>
    /// <param name="deviceIdentifier">The device identifier of the user</param>
    /// <returns>A refresh token</returns>
    public Task<RefreshTokenBo> GenerateRefreshToken(UserBo user, string deviceIdentifier);
    public Task<RefreshTokenBo> GetRefreshTokenByToken(String token);
    /// <summary>
    /// Retrieves the refresh tokens for a given user
    /// </summary>
    /// <param name="user">The user to retrieve the tokens for</param>
    /// <returns>A list of refresh tokens</returns>
    public Task<List<RefreshTokenBo>> GetRefreshTokensForUser(UserBo user);
    public bool isExpired(RefreshTokenBo refreshToken);
    /// <summary>
    /// Deletes the expired refresh tokens in the given list 
    /// </summary>
    /// <param name="userTokens">The list of refresh tokens to check</param>

}