using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.WebAPI.Models.FarmUser;

public class AssignUserToFarmRequestDto
{
    public long CompanyUserId { get; set; }
    public long CompanyId { get; set; }
    public long FarmId { get; set; }
    public long UserId { get; set; }
    public CompanyFarmRoles FarmRole { get; set; }
}