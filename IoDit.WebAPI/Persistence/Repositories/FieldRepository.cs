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
        .Include(f => f.Threshold)
        .ToList().Select(f =>
        new Field
        {
            Id = f.Id,
            Name = f.Name,
            Geofence = f.Geofence,
            Farm = f.Farm,
            Threshold = f.Threshold,
            Devices =
            f.Devices.Select(d => new Device
            {
                DevEUI = d.DevEUI,
                Name = d.Name,
                AppKey = d.AppKey,
                JoinEUI = d.JoinEUI,
                Field = f,
                DeviceData = _context.DeviceData.Where(dd => dd.TimeStamp.ToLocalTime() > DateTime.Now.AddDays(-1).ToLocalTime() && dd.DevEUI == d.DevEUI).ToList()
            }).ToList() ?? new List<Device>()
        }).ToList());


    public async Task<Field> CreateField(Field field)
    {
        field.Farm = await _context.Farms.FindAsync(field.Farm.Id);
        await _context.Fields.AddAsync(field);
        await _context.SaveChangesAsync();
        return field;
    }

    public async Task<Field?> GetFieldById(long id) =>
    await Task.Run(() => _context.Fields
        .Where(f => f.Id == id)
        .Include(f => f.Farm)
        .Include(f => f.Threshold)
        .Include(f => f.Devices).ToList()
        .Select(f =>
        new Field
        {
            Id = f.Id,
            Name = f.Name,
            Geofence = f.Geofence,
            Farm = f.Farm,
            Threshold = f.Threshold,
            Devices =
            f.Devices.Select(d => new Device
            {
                DevEUI = d.DevEUI,
                Name = d.Name,
                AppKey = d.AppKey,
                JoinEUI = d.JoinEUI,
                Field = f,
                DeviceData = _context.DeviceData.Where(dd => dd.TimeStamp.ToLocalTime() > DateTime.Now.AddDays(-1).ToLocalTime() && dd.DevEUI == d.DevEUI).ToList()
            }).ToList()
        }).First(f => f.Id == id));
}