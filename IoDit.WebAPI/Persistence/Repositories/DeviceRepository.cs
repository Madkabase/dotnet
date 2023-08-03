using IoDit.WebAPI.BO;
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

        public async Task<Device> CreateDevice(FieldBo fieldBo, DeviceBo device)
        {
            Device deviceEntity = Device.FromBo(device);
            deviceEntity.FieldId = fieldBo.Id;
            var res = await _dbContext.Devices.AddAsync(deviceEntity);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }
    }

}