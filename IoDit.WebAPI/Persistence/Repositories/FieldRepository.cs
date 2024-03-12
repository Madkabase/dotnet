using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class FieldRepository : IFieldRepository
{
    private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly AgroditDbContext _context;
    public FieldRepository(AgroditDbContext context)
    {
        _context = context;
    }

    public async Task<List<Field>> GetFieldsByFarm(FarmBo farm) =>
    await Task.Run(() => _context.Fields.Where(f => f.Farm.Id == farm.Id).ToList());

    /// <summary>
    /// Get all fields with devices and device data for the last 24 hours
    /// </summary>
    /// <param name="farm">The farm we want to have the fields with data</param>
    /// <returns>a list of fields with the devices an the data</returns>
    public async Task<List<Field>> GetFieldsWithDevicesByFarm(FarmBo farm) =>
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
                Location = d.Location,
                DeviceDatas = _context.DeviceData
                    .Where(dd => dd.TimeStamp.ToUniversalTime() > DateTime.Now.AddDays(-1).ToUniversalTime() && dd.DevEUI == d.DevEUI)
                    .OrderByDescending(d => d.TimeStamp)
                    .ToList()
            }).ToList() ?? new List<Device>()
        }).ToList());


    public Field? CreateField(FarmBo farmBo, FieldBo fieldBo)
    {
        var fieldE = Field.FromBo(fieldBo);
        fieldE.FarmId = farmBo.Id;
        fieldE = _context.Fields.Add(fieldE).Entity;
        _context.SaveChanges();

        return fieldE;
    }

    public async Task<Field?> GetFieldByIdFull(long id) =>
        await Task.Run(() =>
        {
            Field? field = _context.Fields
            .Include(f => f.Farm)
            .Include(f => f.Threshold)
            .Include(f => f.Devices).ToList()
            .Find(match: f => f.Id == id);

            field.Devices = field?.Devices.Select(d => new Device
            {
                DevEUI = d.DevEUI,
                Name = d.Name,
                AppKey = d.AppKey,
                JoinEUI = d.JoinEUI,
                Field = field,
                Location = d.Location,
                DeviceDatas = _context.DeviceData
                    .Where(dd => dd.DevEUI == d.DevEUI)
                    .Where(dd => dd.TimeStamp.ToLocalTime() > DateTime.Now.AddDays(-1).ToLocalTime())
                    .OrderByDescending(d => d.TimeStamp)
                    .ToList()
            }).ToList() ?? new List<Device>();
            return field;
        });

    public Task<Field?> GetFieldByDeviceEui(string deviceEui)
    {
        return _context.Devices
            .Include(d => d.Field)
            .FirstOrDefaultAsync(d => d.DevEUI == deviceEui)
            .ContinueWith(t => t.Result?.Field);
    }

    public async Task DeleteField(long fieldId)
    {
        _logger.Info($"Deleting field with id {fieldId}");
        // transform this raw sql into an ef query
        // string sql = $"DELETE FROM \"Fields\" WHERE \"Id\" = {fieldId}";
        // await _context.Database.ExecuteSqlRawAsync(sql);
        _context.Fields.Remove(new()
        {
            Id = fieldId
        });
        await _context.SaveChangesAsync();
    }
}