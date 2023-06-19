using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public class RefreshTokenRepository
{
    public AgroditDbContext DbContext { get; }

    public RefreshTokenRepository(AgroditDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<List<RefreshToken>> GetRefreshTokensForUser(User User)
    {
        return await Task.Run(() => DbContext.RefreshTokens.Where(rt => rt.User == User).ToList());
    }

    public async Task<bool> DoesRefreshTokenExist(string token) =>
        await Task.Run(() => !DbContext.RefreshTokens.Any(rt => rt.Token == token));

}
