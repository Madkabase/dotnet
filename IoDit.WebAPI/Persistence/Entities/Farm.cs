using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class Farm : EntityBase, IEntity
{
    public string Name { get; set; }
    public User Owner { get; set; }
    public String AppId { get; set; }
    public string AppName { get; set; }
    public int MaxDevices { get; set; }
    public ICollection<Field> Fields { get; set; } = new List<Field>();
    public ICollection<ThresholdPreset> ThresholdPresets { get; set; } = new List<ThresholdPreset>();
    public ICollection<FarmUser> FarmUsers { get; set; } = new List<FarmUser>();


}