using System.Security.Cryptography;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public interface IRefreshJwtService
{

    /// <summary>
    /// Generates a new refresh token for a user
    /// </summary>
    /// <param name="user">The user to generate the token for</param>
    /// <param name="deviceIdentifier">The device identifier of the user</param>
    /// <returns>A refresh token</returns>
    public Task<RefreshToken> GenerateRefreshToken(User user, string deviceIdentifier);
    public Task<RefreshToken?> GetRefreshTokenByToken(String token);
    /// <summary>
    /// Retrieves the refresh tokens for a given user
    /// </summary>
    /// <param name="user">The user to retrieve the tokens for</param>
    /// <returns>A list of refresh tokens</returns>
    public Task<List<RefreshToken>> GetRefreshTokensForUser(User user);
    public Task<bool> isExpired(RefreshToken refreshToken);
    /// <summary>
    /// Deletes the expired refresh tokens in the given list 
    /// </summary>
    /// <param name="userTokens">The list of refresh tokens to check</param>

}