using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public interface IDeviceService
{
    public Task<DeviceBo> CreateDevice(FieldBo fieldBo, DeviceBo deviceBo);
    Task<DeviceBo> GetDeviceByDevEUI(string devEUI);
}