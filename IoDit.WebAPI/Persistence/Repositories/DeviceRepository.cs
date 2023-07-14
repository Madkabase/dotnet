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

        public async Task CreateDevice(Device device)
        {
            await _dbContext.Devices.AddAsync(device);
            await _dbContext.SaveChangesAsync();
        }
    }

}