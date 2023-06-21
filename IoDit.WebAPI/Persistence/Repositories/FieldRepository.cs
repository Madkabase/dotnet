using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class FieldRepository
{
    private readonly AgroditDbContext _context;
    public FieldRepository(AgroditDbContext context)
    {
        _context = context;
    }

    internal async Task<List<Field>> GetFieldsByFarm(Farm farm) =>
    await Task.Run(() => _context.Fields.Where(f => f.Farm.Id == farm.Id).ToList());

    internal async Task<List<Field>> GetFieldsWithDevicesByFarm(Farm farm) =>
    await Task.Run(() => _context.Fields.Include(f => f.Devices).ThenInclude(d => d.DeviceData).Where(f => f.Farm.Id == farm.Id).ToList());

}