using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class Device : EntityBase, IEntity
{
    public string Name { get; set; }
    public string DevEUI { get; set; }
    public string JoinEUI { get; set; }
    public string AppKey { get; set; }
    public Field Field { get; set; }
    public ICollection<DeviceData> DeviceData { get; set; } = new List<DeviceData>();
}