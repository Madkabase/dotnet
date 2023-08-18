using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class DeviceDataRepository : IDeviceDataRepository
{

    private readonly AgroditDbContext _context;

    public DeviceDataRepository(AgroditDbContext context)
    {
        _context = context;
    }
    public Task<List<DeviceData>> GetDeviceDatasByDevice(DeviceBo deviceBo, DateTime startDate, DateTime endDate)
    {
        return _context.DeviceData.Where(dd => dd.DevEUI == deviceBo.DevEUI)
        .Where(dd => dd.TimeStamp.ToUniversalTime() >= startDate.ToUniversalTime() && dd.TimeStamp.ToUniversalTime() <= endDate.ToUniversalTime()).ToListAsync();

    }
}