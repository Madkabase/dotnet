using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.User;

public class FieldUserDto
{
    public long? Id { get; set; } = 0;
    public FieldDto Field { get; set; } = new FieldDto();
    public UserDto User { get; set; } = new UserDto();
    public FieldRoles Role { get; set; } = FieldRoles.User;
}