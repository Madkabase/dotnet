using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.WebAPI.DTO.User;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AppRoles AppRole { get; set; }
    public string Email { get; set; }
    public long Id { get; set; }
    public List<FarmDTO> Farms { get; set; } = new List<FarmDTO>();
}