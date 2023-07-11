using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IRefreshTokenRepository
{

    public Task<List<RefreshToken>> GetRefreshTokensForUser(User User);

    public Task<bool> DoesRefreshTokenExist(string token);

    public Task<RefreshToken?> GetRefreshTokenByToken(string token);

}
