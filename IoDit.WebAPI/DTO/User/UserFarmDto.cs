using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.User
{
    public class UserFarmDto
    {
        public FarmRoles Role { get; set; }
        public FarmDTO Farm { get; set; } = new FarmDTO();
        public UserDto? User { get; set; } = null;


        public static UserFarmDto FromEntity(FarmUser farmUser) => new UserFarmDto
        {
            Role = farmUser.FarmRole,
            Farm = FarmDTO.FromEntity(farmUser.Farm)
        };
    }
}