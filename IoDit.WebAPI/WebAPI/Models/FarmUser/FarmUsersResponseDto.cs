using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.WebAPI.Models.FarmUser;

public class GetFarmUsersResponseDto
{
    public long Id { get; set; }
    public long CompanyId { get; set; }
    public long CompanyUserId { get; set; }
    public long CompanyFarmId { get; set; }
    public CompanyFarmRoles CompanyFarmRole { get; set; }
}