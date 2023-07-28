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
    Task.Run(() => _context.FarmUsers.Include(fu => fu.Farm).ThenInclude(f => f.Owner).Where(fu => fu.User.Id == user.Id).ToList());

    public Task<Farm?> getFarmDetailsById2(long farmId) =>
    Task.Run(() => _context.Farms
        .Where(f => f.Id == farmId)
        .Include(f => f.Owner)
        .Include(f => f.FarmUsers)
        .ThenInclude(fu => fu.User)
        // add Fields' Thresholds
        .Include(f => f.Fields)
        .ThenInclude(f => f.Threshold)
        .Include(f => f.Fields)
        .FirstOrDefault()
    );

    public Task<Farm?> getFarmDetailsById(long farmId)
    {
        var farm = _context.Farms
            .Where(f => f.Id == farmId)
            .Include(f => f.Owner)
            .Include(f => f.FarmUsers)
            .ThenInclude(fu => fu.User)
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
                        .OrderByDescending(dd => dd.TimeStamp)
                        .FirstOrDefault();
                    if (lastData != null)
                    {
                        device.DeviceData = new List<DeviceData>() { lastData };
                    }
                }
            }
        }
        return Task.FromResult(farm);
    }

}