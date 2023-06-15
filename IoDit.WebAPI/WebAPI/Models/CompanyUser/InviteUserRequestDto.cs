using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.WebAPI.Models.CompanyUser;

public class InviteUserRequestDto
{
    public string Email { get; set; }
    public long CompanyUserId { get; set; }
    public long CompanyId { get; set; }
    public CompanyRoles CompanyRole { get; set; }
}