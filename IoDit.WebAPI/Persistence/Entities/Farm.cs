using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class Farm : EntityBase, IEntity
{
    public string Name { get; set; }
    public long OwnerId { get; set; }
    public User Owner { get; set; } = new User();
    public string AppId { get; set; }
    public string AppName { get; set; }
    public int MaxDevices { get; set; }
    public ICollection<Field> Fields { get; set; } = new List<Field>();
    public ICollection<ThresholdPreset> ThresholdPresets { get; set; } = new List<ThresholdPreset>();
    public ICollection<FarmUser> FarmUsers { get; set; } = new List<FarmUser>();


    public static Farm FromDto(FarmDto farmDto)
    {
        return new Farm
        {
            Id = farmDto.Id,
            Name = farmDto.Name,
            AppId = farmDto.AppId,
            AppName = farmDto.AppName,
            MaxDevices = farmDto.MaxDevices,
            Owner = farmDto.Owner != null ? User.FromDTO(farmDto.Owner) : new User()
        };
    }

    public static Farm FromBo(FarmBo farmBo)
    {
        return new Farm
        {
            Id = farmBo.Id,
            Name = farmBo.Name,
            AppId = farmBo.AppId,
            AppName = farmBo.AppName,
            MaxDevices = farmBo.MaxDevices,
            Owner = farmBo.Owner != null ? User.FromBo(farmBo.Owner) : new User()
        };
    }

}