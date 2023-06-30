using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class FieldRepository : IFieldRepository
{
    private readonly AgroditDbContext _context;
    public FieldRepository(AgroditDbContext context)
    {
        _context = context;
    }

    public async Task<List<Field>> GetFieldsByFarm(Farm farm) =>
    await Task.Run(() => _context.Fields.Where(f => f.Farm.Id == farm.Id).ToList());

    /// <summary>
    /// Get all fields with devices and device data for the last 24 hours
    /// </summary>
    /// <param name="farm">The farm we want to have the fields with data</param>
    /// <returns>a list of fields with the devices an the data</returns>
    public async Task<List<Field>> GetFieldsWithDevicesByFarm(Farm farm) =>
    await Task.Run(() =>
    _context.Fields.Where(f => f.Farm.Id == farm.Id)
        .Include(f => f.Devices)
        .ThenInclude(d => d.DeviceData.Where(dd => dd.TimeStamp.ToLocalTime() > DateTime.Now.AddDays(-1).ToLocalTime()))
        .ToList());

}