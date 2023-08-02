using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.User
{
    public class FarmUserDto
    {
        public FarmRoles Role { get; set; }
        public FarmDto Farm { get; set; } = new FarmDto();
        public UserDto? User { get; set; } = new UserDto();
    };
}