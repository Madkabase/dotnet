using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.User
{
    public class FarmUserDto
    {
        public FarmRoles Role { get; set; }
        public FarmDTO Farm { get; set; } = new FarmDTO();
        public UserDto User { get; set; } = new UserDto();


        public static FarmUserDto FromEntity(FarmUser farmUser) => new FarmUserDto
        {
            Role = farmUser.FarmRole,
            Farm = FarmDTO.FromEntity(farmUser.Farm)
        };
    }
}