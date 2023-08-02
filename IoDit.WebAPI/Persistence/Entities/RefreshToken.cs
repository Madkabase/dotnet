using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class RefreshToken : EntityBase, IEntity
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string DeviceIdentifier { get; set; }

    internal static RefreshToken FromBo(RefreshTokenBo refreshTokenBo)
    {
        return new RefreshToken
        {
            Id = refreshTokenBo.Id,
            Token = refreshTokenBo.Token,
            Expires = refreshTokenBo.Expires,
            DeviceIdentifier = refreshTokenBo.DeviceIdentifier
        };
    }
}