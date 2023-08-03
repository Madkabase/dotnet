using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.User;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AppRoles AppRole { get; set; }
    public string Email { get; set; }
    public long Id { get; set; }
    public List<FarmUserDto> Farms { get; set; } = new List<FarmUserDto>();

    public static UserDto FromBo(BO.UserBo user)
    {
        return new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            AppRole = user.AppRole,
            Email = user.Email,
            Id = user.Id
        };
    }
}