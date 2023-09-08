using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public interface IDeviceService
{
    public Task<DeviceBo> CreateDevice(FieldBo fieldBo, DeviceBo deviceBo);
    Task DeleteDevice(string devEUI, FieldBo fieldBo);
    Task DeleteDevicesFromField(long fieldId);
    Task<DeviceBo> GetDeviceByDevEUI(string devEUI);
    /// <summary>
    /// get the devices from a field
    /// </summary>
    /// <param name="fieldBo">field which devices are attached</param>
    /// <returns></returns>
    Task<List<DeviceBo>> GetFieldDevices(FieldBo fieldBo);
}