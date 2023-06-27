using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.DTO.User;

namespace IoDit.WebAPI.DTO.Farm;

public class FarmDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public UserDto? Owner { get; set; }

    public String AppId { get; set; } = "";
    public string AppName { get; set; } = "";
    public int MaxDevices { get; set; }
    public List<UserFarmDto>? Users { get; set; }
    public List<FieldDto>? Fields { get; set; }



    public static FarmDTO FromEntity(Persistence.Entities.Farm farm)
    {
        return new FarmDTO
        {
            Id = farm.Id,
            Name = farm.Name,
            Owner = UserDto.FromEntity(farm.Owner),
            AppId = farm.AppId,
            AppName = farm.AppName,
            MaxDevices = farm.MaxDevices,
            Users = farm.FarmUsers.Select(fu => new UserFarmDto
            {
                Role = fu.FarmRole,
                User = UserDto.FromEntity(fu.User)
            }).ToList(),
            Fields = farm.Fields.Select(f => FieldDto.FromEntity(f)).ToList()
        };
    }
}