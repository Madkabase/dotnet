using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IRefreshTokenRepository
{

    public Task<List<RefreshToken>> GetRefreshTokensForUser(UserBo User);

    public Task<bool> DoesRefreshTokenExist(string token);

    public Task<RefreshToken?> GetRefreshTokenByToken(string token);
    public Task UpdateToken(RefreshTokenBo currentToken);
    public Task CreateRefreshToken(RefreshTokenBo refreshToken);

    public Task<List<RefreshToken>> GetRefreshTokensOfFarmAdmins(long farmId);
}
