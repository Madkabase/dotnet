using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.BO;

public class RefreshTokenBo
{
    public UserBo User { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string DeviceIdentifier { get; set; }

    public RefreshTokenBo()
    {
        User = new UserBo();
        Token = "";
        Expires = DateTime.Now;
        DeviceIdentifier = "";
    }

    public RefreshTokenBo(UserBo user, string token, DateTime expires, string deviceIdentifier)
    {
        User = user;
        Token = token;
        Expires = expires;
        DeviceIdentifier = deviceIdentifier;
    }

    // fromEntity
    public static RefreshTokenBo FromEntity(RefreshToken refreshToken)
    {
        return new RefreshTokenBo(
            UserBo.FromEntity(refreshToken.User),
            refreshToken.Token,
            refreshToken.Expires,
            refreshToken.DeviceIdentifier
        );
    }



}