using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.BO;

public class FieldUserBo
{
    public long Id { get; set; } = 0;
    public FieldBo Field { get; set; } = new FieldBo();
    public UserBo User { get; set; } = new UserBo();
    public FieldRoles FieldRole { get; set; } = FieldRoles.User;

    // from Entity 
    public static FieldUserBo FromEntity(Persistence.Entities.FieldUser fieldUser)
    {
        var bo = new FieldUserBo
        {
            Id = fieldUser.Id,
            Field = FieldBo.FromEntity(fieldUser.Field ?? new Persistence.Entities.Field() { Id = fieldUser.FieldId }),
            User = UserBo.FromEntity(fieldUser.User ?? new Persistence.Entities.User() { Id = fieldUser.UserId }),
            FieldRole = fieldUser.FieldRole
        };

        bo.Field.Id = fieldUser.FieldId;
        bo.User.Id = fieldUser.UserId;

        return bo;
    }
    // from DTO
    public static FieldUserBo FromDto(DTO.User.FieldUserDto fieldUserDto)
    {
        return new FieldUserBo
        {
            Id = fieldUserDto.Id ?? 0,
            Field = FieldBo.FromDto(fieldUserDto.Field),
            User = fieldUserDto.User != null ? UserBo.FromDto(fieldUserDto.User) : new UserBo(),
            FieldRole = fieldUserDto.Role
        };
    }

}