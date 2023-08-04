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

    public FieldService(IFieldRepository fieldRepository,
        IFarmUserService farmUserService,
        IFarmService farmService
    )
    {
        _fieldRepository = fieldRepository;
        _farmUserService = farmUserService;
        _farmService = farmService;
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

        // TODO : when FieldUser is created, implement this

        var d = await _farmUserService.HasAccessToFarm(await _farmService.GetFarmByFieldId(fieldId), user);

        return d;
    }

    public async Task<bool> UserCanChangeField(long fieldId, UserBo user)
    {
        FarmBo farm = await _farmService.GetFarmByFieldId(fieldId);

        // TODO : when FieldUser is created, implement this
        FarmUserBo farmUser = await _farmUserService.GetUserFarm(farm.Id, user.Id);
        if (farmUser == null)
        {
            return false;
        }
        return farmUser.FarmRole == Utilities.Types.FarmRoles.Admin;
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