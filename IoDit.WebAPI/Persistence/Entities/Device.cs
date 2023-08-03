using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class Device : IEntity
{
    // devEUI is the unique identifier for the device
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string DevEUI { get; set; }
    public string Name { get; set; }
    public string JoinEUI { get; set; }
    public string AppKey { get; set; }
    public long FieldId { get; set; }
    public Field Field { get; set; }
    // virtual link on the deviceEUI property in the DeviceData class with no db link
    [NotMapped]
    public ICollection<DeviceData> DeviceData { get; set; } = new List<DeviceData>();

    internal static Device FromBo(DeviceBo device)
    {
        return new Device
        {
            DevEUI = device.DevEUI,
            Name = device.Name,
            JoinEUI = device.JoinEUI,
            AppKey = device.AppKey,
        };
    }
}