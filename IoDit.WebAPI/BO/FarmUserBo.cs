using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.BO;

public class FarmUserBo
{
    public long Id { get; set; }
    public FarmBo Farm { get; set; }
    public UserBo User { get; set; }
    public FarmRoles FarmRole { get; set; }

    public FarmUserBo()
    {
        Id = 0;
        Farm = new FarmBo();
        User = new UserBo();
        FarmRole = FarmRoles.Invited;
    }

    public FarmUserBo(long id, FarmBo farm, UserBo user, FarmRoles farmRole)
    {
        Id = id;
        Farm = farm;
        User = user;
        FarmRole = farmRole;
    }

    public static FarmUserBo FromDto(DTO.User.FarmUserDto farmUserDto)
    {
        return new FarmUserBo
        {
            Id = farmUserDto.Id ?? 0,
            Farm = FarmBo.FromDto(farmUserDto.Farm),
            User = farmUserDto.User != null ? UserBo.FromDto(farmUserDto.User) : new UserBo(),
            FarmRole = farmUserDto.Role
        };
    }

    public static FarmUserBo FromEntity(Persistence.Entities.FarmUser farmUser)
    {
        var bo = new FarmUserBo
        {
            Id = farmUser.Id,

            Farm = FarmBo.FromEntity(farmUser.Farm ?? new Persistence.Entities.Farm() { Id = farmUser.FarmId, OwnerId = farmUser.UserId }),
            User = UserBo.FromEntity(farmUser.User ?? new Persistence.Entities.User() { Id = farmUser.UserId }),
            FarmRole = farmUser.FarmRole
        };

        bo.Farm.Id = farmUser.FarmId;
        bo.User.Id = farmUser.UserId;

        return bo;
    }

}