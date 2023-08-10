using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Persistence.Entities;

public class FieldUser : EntityBase, IEntity
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long FieldId { get; set; }
    public Field Field { get; set; }
    public FieldRoles FieldRole { get; set; }

    public static FieldUser FromBo(FieldUserBo fieldUser)
    {
        return new FieldUser
        {
            Id = fieldUser.Id,
            FieldRole = fieldUser.FieldRole,
            FieldId = fieldUser.Field.Id,
            Field = Field.FromBo(fieldUser.Field),
            UserId = fieldUser.User.Id,
            User = User.FromBo(fieldUser.User)
        };
    }
}