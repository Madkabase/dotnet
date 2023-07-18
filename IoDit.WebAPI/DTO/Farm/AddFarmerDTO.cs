using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.Farm;

public class AddFarmerDTO
{
    public string UserEmail { get; set; }
    public FarmRoles Role { get; set; }
}
