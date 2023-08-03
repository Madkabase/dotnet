using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    public AgroditDbContext DbContext { get; }

    public RefreshTokenRepository(AgroditDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<List<RefreshToken>> GetRefreshTokensForUser(UserBo User) =>
        await Task.Run(() => DbContext.RefreshTokens.Include(rt => rt.User).Where(rt => rt.User.Id == User.Id).ToList());


    public async Task<bool> DoesRefreshTokenExist(string token) =>
        await Task.Run(() => !DbContext.RefreshTokens.Any(rt => rt.Token == token));

    public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
      => await Task.Run(() => DbContext.RefreshTokens.Include(rt => rt.User).FirstOrDefault(rt => rt.Token == token));

    public async Task UpdateToken(RefreshTokenBo currentToken)
    {
        // update exires, token
        string sql = "UPDATE \"RefreshTokens\" SET \"Expires\" = '{0}', \"Token\" = '{1}' WHERE \"Id\" = {2}";
        sql = string.Format(sql, currentToken.Expires.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff zzz").Remove(27, 1), currentToken.Token, currentToken.Id);
        await DbContext.Database.ExecuteSqlRawAsync(sql);
        // DbContext.RefreshTokens.Update(RefreshToken.FromBo(currentToken));
        // await DbContext.SaveChangesAsync();
    }


    public async Task CreateRefreshToken(RefreshTokenBo refreshToken)
    {
        var param = new object[] {
            refreshToken.User.Id,
            refreshToken.Token,
            refreshToken.Expires.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff zzz").Remove(27, 1),
            refreshToken.DeviceIdentifier
            };
        string sql = "INSERT INTO \"RefreshTokens\" ( \"UserId\", \"Token\", \"Expires\", \"DeviceIdentifier\") VALUES "
        + $"({param[0]}, '{param[1]}', '{param[2]}', '{param[3]}')";
        // + "({0}, '{1}', '{2}', '{3}')";
        // + $"(?, '?', '?',' ?')";

        await DbContext.Database.ExecuteSqlRawAsync(sql);
    }
}
