using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.WebAPI.Models.User;

public class UserResponseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AppRoles AppRole { get; set; }
    public string Email { get; set; }
    public long Id { get; set; }
}