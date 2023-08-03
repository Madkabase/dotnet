using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class FieldService : IFieldService
{

    private readonly IFieldRepository _fieldRepository;
    private readonly IFarmUserService _farmUserService;
    private readonly IFarmService _farmService;
    private readonly IThresholdService _thresholdService;

    public FieldService(IFieldRepository fieldRepository,
        IFarmUserService farmUserService,
        IFarmService farmService,
        IThresholdService thresholdService
    )
    {
        _fieldRepository = fieldRepository;
        _farmUserService = farmUserService;
        _farmService = farmService;
        _thresholdService = thresholdService;
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

        var fieldCreated = _fieldRepository.CreateField(farm, field);
        if (fieldCreated == null)
        {
            throw new Exception("Field not created");
        }

        field.Id = fieldCreated.Id;

        return Task.Run(() => field);
    }

    public async Task<FieldBo> GetFieldById(long id)
    {
        var field = await _fieldRepository.GetFieldById(id)
            ?? throw new EntityNotFoundException($"Field with id {id} not found");
        return FieldBo.FromEntity(field);
    }

    public async Task<bool> UserHasAccessToField(long fieldId, UserBo user)
    {
        var field = (await GetFieldById(fieldId));

        // TODO : when FieldUser is created, implement this

        var d = await _farmUserService.HasAccessToFarm(await _farmService.GetFarmByFieldId(fieldId), user);

        return d;
    }

    public async Task<bool> UserCanChangeField(long fieldId, UserBo user)
    {
        var field = await _fieldRepository.GetFieldById(fieldId);
        if (field == null)
        {
            return false;
        }
        // TODO : when FieldUser is created, implement this

        var d = await _farmUserService.GetUserFarm(field.Farm.Id, user.Id);
        if (d == null)
        {
            return false;
        }
        return d.FarmRole == Utilities.Types.FarmRoles.Admin;
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
}