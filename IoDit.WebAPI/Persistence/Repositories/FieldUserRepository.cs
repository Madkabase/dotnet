using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class FieldUserRepository : IFieldUserRepository
{
    private readonly AgroditDbContext _context;
    private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    public FieldUserRepository(AgroditDbContext context)
    {
        _context = context;
    }

    public Task<List<FieldUser>> GetFieldsByUser(UserBo user)
    {
        return Task.Run(() => _context.FieldUsers
            .Where(fu => fu.UserId == user.Id)
            .Include(f => f.Field)
            .ThenInclude(f => f.Threshold)
            .Include(fu => fu.User)
            .ToList()
        );
    }

    public Task<List<FieldUser>> GetUsersByField(FieldBo field)
    {
        return Task.Run(() => _context.FieldUsers
            .Where(fu => fu.FieldId == field.Id)
            .Include(f => f.Field)
            .ThenInclude(f => f.Threshold)
            .Include(fu => fu.User)
            .ToList()
        );
    }

    public async Task<FieldUser> AddFieldUser(FieldUserBo fieldUserBo)
    {
        FieldUser fu = new()
        {
            FieldId = fieldUserBo.Field.Id,
            UserId = fieldUserBo.User.Id,
            FieldRole = fieldUserBo.FieldRole
        };

        var user = _context.FieldUsers.Add(fu).Entity;

        await _context.SaveChangesAsync();
        return user;
    }

    public Task RemoveFieldUser(FieldUserBo fieldUserBo) =>
    Task.Run(() =>
       {
           _context.FieldUsers.Remove(FieldUser.FromBo(fieldUserBo));
           _context.SaveChanges();
       }
    );

    public Task<FieldUser?> GetFieldUser(long fieldId, long userId)
    {
        return Task.Run(() => _context.FieldUsers.
        Include(fu => fu.Field)
        .Include(fu => fu.User)
        .FirstOrDefault(fu => fu.FieldId == fieldId && fu.UserId == userId));
    }

    public async Task<List<FieldUser>> GetFieldsWithDevicesByUser(UserBo user)
    {
        var userFields = await _context.FieldUsers
            .Where(fu => fu.UserId == user.Id)
            .Include(f => f.Field)
            .ThenInclude(f => f.Threshold)
            .Include(fu => fu.Field)
            .ThenInclude(f => f.Devices)
            .ToListAsync();

        foreach (var uf in userFields)
        {
            foreach (var device in uf.Field.Devices)
            {
                device.DeviceDatas = await _context.DeviceData
                    .Where(dd => dd.DevEUI == device.DevEUI)
                    .Where(dd => dd.TimeStamp.ToUniversalTime() > DateTime.Now.ToUniversalTime().AddDays(-1))

                    .OrderByDescending(dd => dd.TimeStamp)
                    .ToListAsync();
            }
        }

        return userFields;
    }

}