using System.Collections.Generic;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class FarmRepository : IFarmRepository
{
    private readonly AgroditDbContext _context;
    public FarmRepository(AgroditDbContext context)
    {
        _context = context;
    }

    public Task<List<FarmUser>> getUserFarms(User user) =>
    Task.Run(() => _context.FarmUsers
        .Where(fu => fu.User.Id == user.Id)
        .Include(fu => fu.Farm)
        .ThenInclude(f => f.Owner)
        .ToList()
    );

    public Task<Farm?> getFarmDetailsById(long farmId)
    {
        var farm = _context.Farms
            .Where(f => f.Id == farmId)
            .Include(f => f.Owner)
            // add Fields' Thresholds
            .Include(f => f.Fields)
            .ThenInclude(f => f.Threshold)
            .Include(f => f.Fields)
            .ThenInclude(f => f.Devices)
            .FirstOrDefault();
        // add last device data to each Device
        if (farm != null)
        {
            foreach (var field in farm.Fields)
            {
                foreach (var device in field.Devices)
                {
                    var lastData = _context.DeviceData
                        .Where(dd => dd.DevEUI == device.DevEUI)
                        .Where(dd => dd.TimeStamp.ToUniversalTime() > DateTime.Now.AddHours(-24).ToUniversalTime())
                        .OrderByDescending(dd => dd.TimeStamp)
                        .FirstOrDefault();
                    if (lastData != null)
                    {
                        device.DeviceDatas = new List<DeviceData>() { lastData };
                    }
                }
            }
        }
        return Task.FromResult(farm);
    }

    public Task<Farm?> getFarmByFieldId(long fieldId)
    {
        var farm = _context.Farms.Where(f => f.Fields.Any(f => f.Id == fieldId)).FirstOrDefault();
        return Task.FromResult(farm);
    }
}