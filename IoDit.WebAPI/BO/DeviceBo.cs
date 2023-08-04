using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.BO;

public class DeviceBo
{

    public string DevEUI { get; set; }
    public string Name { get; set; }
    public string JoinEUI { get; set; }
    public string AppKey { get; set; }
    // virtual link on the deviceEUI property in the DeviceData class with no db link

    public ICollection<DeviceDataBo> DeviceData { get; set; }

    public DeviceBo()
    {
        DevEUI = "";
        Name = "";
        JoinEUI = "";
        AppKey = "";
        DeviceData = new List<DeviceDataBo>();
    }
    public DeviceBo(string devEUI, string name, string joinEUI, string appKey, /*FieldBo field,*/ ICollection<DeviceDataBo> deviceData)
    {
        DevEUI = devEUI;
        Name = name;
        JoinEUI = joinEUI;
        AppKey = appKey;
        DeviceData = deviceData;
    }

    public static DeviceBo FromEntity(Device device)
    {
        return new DeviceBo
        (
            devEUI: device.DevEUI,
            name: device.Name,
            joinEUI: device.JoinEUI,
            appKey: device.AppKey,
            // field: FieldBo.FromEntity(device.Field),
            deviceData: device.DeviceDatas.Select(dd => DeviceDataBo.FromEntity(dd)).ToList()
        );
    }
    public static DeviceBo FromDTO(DTO.Device.DeviceDto device)
    {
        return new DeviceBo(device.Id, device.Name, "", "", /*new FieldBo(),*/ device.Data.Select(d => DeviceDataBo.FromDto(d)).ToList());
    }
}