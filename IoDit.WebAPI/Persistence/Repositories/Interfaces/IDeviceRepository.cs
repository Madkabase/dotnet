using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories
{
    public interface IDeviceRepository
    {
        public Task<Device> CreateDevice(Device device);
    }
}