using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories
{
    public interface IDeviceRepository
    {
        public Task<Device> CreateDevice(FieldBo fieldBo, DeviceBo device);
        public Task<Device?> GetDeviceByDevEUI(string devEUI);
    }
}