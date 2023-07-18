using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AgroditDbContext _dbContext;

        public DeviceRepository(AgroditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Device?> GetDeviceByDevEUI(string devEUI)
        {
            return await _dbContext.Devices.FirstOrDefaultAsync(d => d.DevEUI == devEUI);
        }

        public async Task<Device> CreateDevice(Device device)
        {
            var res = await _dbContext.Devices.AddAsync(device);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }
    }

}