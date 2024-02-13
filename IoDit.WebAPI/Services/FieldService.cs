using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.Utilities.Helpers;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Services;

public class FieldService : IFieldService
{

    private readonly IFieldRepository _fieldRepository;
    private readonly IFarmUserService _farmUserService;
    private readonly IFarmService _farmService;
    private readonly IFieldUserService _fieldUserService;
    private readonly NotificationsHelper _notificationsHelper;
    private readonly IRefreshJwtService _refreshTokenService;
    private readonly IThresholdService _thresholdService;
    private readonly IAlertService _alertService;
    private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly AgroditDbContext _context;
    private readonly IDeviceService _deviceService;


    public FieldService(
        IFieldRepository fieldRepository,
        IFarmUserService farmUserService,
        IFarmService farmService,
        IFieldUserService fieldUserService,
        NotificationsHelper notificationsHelper,
        IRefreshJwtService refreshTokenService,
        IThresholdService thresholdService,
        IAlertService alertService,
        AgroditDbContext context,
        IDeviceService deviceService
    )
    {
        _fieldRepository = fieldRepository;
        _farmUserService = farmUserService;
        _farmService = farmService;
        _fieldUserService = fieldUserService;
        _notificationsHelper = notificationsHelper;
        _refreshTokenService = refreshTokenService;
        _thresholdService = thresholdService;
        _alertService = alertService;
        _context = context;
        _deviceService = deviceService;
    }

    public async Task<List<FieldBo>> GetFieldsForFarm(FarmBo farm)
    {
        var fields = await _fieldRepository.GetFieldsByFarm(farm);
        if (fields == null)
        {
            return new List<FieldBo>();
        }
        return fields.Select(f => FieldBo.FromEntity(f)).ToList();
    }

    public async Task<List<FieldBo>> GetFieldsWithDevicesForFarm(FarmBo farm)
    {

        var fields = (await _fieldRepository.GetFieldsWithDevicesByFarm(farm)).Select(f => FieldBo.FromEntity(f)).ToList();
        if (fields == null)
        {
            return new List<FieldBo>();
        }
        return fields;


    }

    public Task<FieldBo> CreateFieldForFarm(FieldBo field, FarmBo farm)
    {

        var fieldCreated = _fieldRepository.CreateField(farm, field)
            ?? throw new Exception("Field not created");
        field.Id = fieldCreated.Id;

        return Task.Run(() => field);
    }

    public async Task<FieldBo> GetFieldByIdFull(long id)
    {
        var field = await _fieldRepository.GetFieldByIdFull(id)
            ?? throw new EntityNotFoundException($"Field with id {id} not found");
        return FieldBo.FromEntity(field);
    }

    public async Task<bool> UserHasAccessToField(long fieldId, UserBo user)
    {
        bool hasAccess = false;
        try
        {
            await _fieldUserService.GetUserField(fieldId, user.Id);
            hasAccess = true;
        }
        catch (System.Exception)
        {

        }

        var d = await _farmUserService.HasAccessToFarm(await _farmService.GetFarmByFieldId(fieldId), user);

        return d || hasAccess;
    }

    public async Task<bool> UserCanChangeField(long fieldId, UserBo user)
    {
        FarmBo farm = await _farmService.GetFarmByFieldId(fieldId);
        try
        {
            FarmUserBo farmUser = await _farmUserService.GetUserFarm(farm.Id, user.Id);
            return farmUser.FarmRole == Utilities.Types.FarmRoles.Admin;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public int CalculateOverAllMoistureLevel(List<DeviceBo> devices, ThresholdBo threshold)
    {
        if (devices.Count == 0)
        {
            return 0;
        }
        var lastDatas = devices.Select(device =>
        {
            if (device.DeviceData.Count == 0)
            {
                return new DeviceDataBo(
                    0,
                    device.DevEUI,
                    0,
                    0,
                    0,
                    0,
                    DateTime.Now
                );
            }
            return device.DeviceData.OrderByDescending(d => d.TimeStamp).First();
        }).ToList();
        if (lastDatas.Count == 0)
        {
            return 0;
        }
        if (threshold.MainSensor == Utilities.Types.MainSensor.SensorDown)
        {
            return lastDatas.Select(d => d.Humidity2).Min();
        }
        else
        {
            return lastDatas.Select(d => d.Humidity1).Min();
        }
    }

    public async Task<FieldBo> GetFieldFromDeviceEui(string devEUI)
    {
        var field = await _fieldRepository.GetFieldByDeviceEui(devEUI)
            ?? throw new EntityNotFoundException($"Field with device eui {devEUI} not found");
        return FieldBo.FromEntity(field);
    }

    public async Task NotifyFarmAdmins(FieldBo field, string v)
    {
        // TODO : think about when the notification is sent.
        //? see =  https://dev.azure.com/agrodit/Agrodit/_workitems/edit/71/
        var farm = await _farmService.GetFarmByFieldId(field.Id);
        // get refresh tokens fopr farm admins
        var tokens = await _refreshTokenService.GetRefreshTokensForFarmAdmins(farm.Id);
        var notif = await _notificationsHelper.sendAlert(tokens.Select(t => t.DeviceIdentifier).ToList(),
        new AndroidNotification(
            title: "Field Alert",
            body: v,
            data: new Dictionary<string, string> { { "fieldId", field.Id.ToString() } },
            additionalHeaders: new Dictionary<string, string> { { "priority", "high" } }
        ));
        _logger.Info($"Sent notification to {tokens.Count} farm admins", notif);
    }

    public async Task DeleteField(long fieldId)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            // do we delete all the devices from the field with all the data ?
            await _deviceService.DeleteDevicesFromField(fieldId);
            await _alertService.DeleteAlertsFromFieldId(fieldId);

            await _thresholdService.DeleteThresholdFromField(fieldId);
            await _fieldRepository.DeleteField(fieldId);
            transaction.Commit();
        }
        catch (System.Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
}
