using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Persistence.Entities;

public class FarmUser : EntityBase, IEntity
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long FarmId { get; set; }
    public Farm Farm { get; set; }
    public FarmRoles FarmRole { get; set; }

    public static FarmUser FromBo(FarmUserBo farmUser)
    {
        return new FarmUser
        {
            Id = farmUser.Id,
            FarmRole = farmUser.FarmRole,
            FarmId = farmUser.Farm.Id,
            Farm = Farm.FromBo(farmUser.Farm),
            UserId = farmUser.User.Id,
            User = User.FromBo(farmUser.User)
        };
    }
}