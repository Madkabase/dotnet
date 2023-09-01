using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public interface IDeviceService
{
    public Task<DeviceBo> CreateDevice(FieldBo fieldBo, DeviceBo deviceBo);
    Task DeleteDevice(string devEUI, FieldBo fieldBo);
    Task DeleteDevicesFromField(long fieldId);
    Task<DeviceBo> GetDeviceByDevEUI(string devEUI);
}