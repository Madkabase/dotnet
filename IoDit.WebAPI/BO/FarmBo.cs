using IoDit.WebAPI.DTO.Farm;

namespace IoDit.WebAPI.BO;

public class FarmBo
{
    public long Id { get; set; }
    public string Name { get; set; }
    public UserBo Owner { get; set; } = new UserBo();
    public string AppId { get; set; }
    public string AppName { get; set; }
    public int MaxDevices { get; set; }
    public ICollection<FieldBo> Fields { get; set; } = new List<FieldBo>();
    public ICollection<ThresholdPresetBo> ThresholdPresets { get; set; } = new List<ThresholdPresetBo>();

    public FarmBo()
    {
        Id = 0;
        Name = "";
        Owner = new UserBo();
        AppId = "";
        AppName = "";
        MaxDevices = 0;
        Fields = new List<FieldBo>();
        ThresholdPresets = new List<ThresholdPresetBo>();
    }
    public FarmBo(long id,
        string name,
        UserBo owner,
        string appId,
        string appName,
        int maxDevices,
        ICollection<FieldBo> fields,
        ICollection<ThresholdPresetBo> thresholdPresets,
        ICollection<FarmUserBo> farmUsers
    )
    {
        Id = id;
        Name = name;
        Owner = owner;
        AppId = appId;
        AppName = appName;
        MaxDevices = maxDevices;
        Fields = fields;
        ThresholdPresets = thresholdPresets;
    }

    public static FarmBo FromDto(FarmDto farmDto)
    {
        return new FarmBo
        {
            Id = farmDto.Id,
            Name = farmDto.Name,
            AppId = farmDto.AppId,
            AppName = farmDto.AppName,
            MaxDevices = farmDto.MaxDevices,
            Owner = UserBo.FromDto(farmDto.Owner)
        };
    }

    public static FarmBo FromEntity(Persistence.Entities.Farm farm)
    {
        return new FarmBo
        {
            Id = farm.Id,
            Name = farm.Name,
            AppId = farm.AppId,
            AppName = farm.AppName,
            MaxDevices = farm.MaxDevices,
            Owner = UserBo.FromEntity(farm.Owner),
            Fields = farm.Fields.Select(FieldBo.FromEntity).ToList(),
        };
    }
}