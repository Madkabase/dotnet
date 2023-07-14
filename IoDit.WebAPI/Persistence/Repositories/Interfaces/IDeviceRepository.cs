using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories
{
    public interface IDeviceRepository
    {
        public Task CreateDevice(Device device);
    }
}