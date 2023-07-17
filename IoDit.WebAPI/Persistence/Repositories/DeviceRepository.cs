using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AgroditDbContext _dbContext;

        public DeviceRepository(AgroditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Device> CreateDevice(Device device)
        {
            var res = await _dbContext.Devices.AddAsync(device);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }
    }

}