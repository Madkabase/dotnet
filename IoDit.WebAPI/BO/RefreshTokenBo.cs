using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.BO;

public class RefreshTokenBo
{
    public long Id { get; set; }
    public UserBo User { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string DeviceIdentifier { get; set; }

    public RefreshTokenBo()
    {
        Id = 0;
        User = new UserBo();
        Token = "";
        Expires = DateTime.Now;
        DeviceIdentifier = "";
    }

    public RefreshTokenBo(long id, UserBo user, string token, DateTime expires, string deviceIdentifier)
    {
        Id = id;
        User = user;
        Token = token;
        Expires = expires;
        DeviceIdentifier = deviceIdentifier;
    }

    // fromEntity
    public static RefreshTokenBo FromEntity(RefreshToken refreshToken)
    {
        return new RefreshTokenBo(
            refreshToken.Id,
            refreshToken.User != null ? UserBo.FromEntity(refreshToken.User) : new UserBo(),
            refreshToken.Token,
            refreshToken.Expires,
            refreshToken.DeviceIdentifier
        );
    }



}