using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.Device;
using IoDit.WebAPI.WebAPI.Models.DeviceData;
using IoDit.WebAPI.WebAPI.Models.Field;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class FieldService : IFieldService
{
    private readonly IUtilsRepository _utilsRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IFarmRepository _farmRepository;
    private readonly IFieldRepository _fieldRepository;

    public FieldService(IUtilsRepository repository,
        ICompanyRepository companyRepository,
        IFarmRepository farmRepository,
        IFieldRepository fieldRepository)
    {
        _utilsRepository = repository;
        _companyRepository = companyRepository;
        _farmRepository = farmRepository;
        _fieldRepository = fieldRepository;
    }

    public async Task<FieldResponseDto> CreateCompanyField(CreateCompanyField request)
    {
        var company = await _companyRepository.GetCompanyById(request.CompanyId);
        var companyFarm = await _farmRepository.GetCompanyFarmById(request.CompanyFarmId);

        var companyField = new CompanyField()
        {
            Company = company,
            Geofence = request.Geofence,
            Name = request.Name,
            CompanyFarm = companyFarm,
            CompanyId = request.CompanyId,
            CompanyFarmId = request.CompanyFarmId
        };
        var createdField = await _utilsRepository.CreateAsync(companyField);
        return new FieldResponseDto()
        {
            Id = createdField.Id,
            CompanyId = createdField.CompanyId,
            CompanyFarmId = createdField.CompanyFarmId,
            Name = createdField.Name,
            Geofence = createdField.Geofence
        };
    }

    public async Task<FieldResponseDto> UpdateGeofence(UpdateGeofence request)
    {
        var companyField = await _fieldRepository.GetFieldById(request.FieldId);
        if (companyField != null)
        {
            companyField.Geofence = request.Geofence;
            var updated = await _utilsRepository.UpdateAsync(companyField);
            return new FieldResponseDto()
            {
                Id = updated.Id,
                CompanyId = updated.CompanyId,
                CompanyFarmId = updated.CompanyFarmId,
                Name = updated.Name,
                Geofence = updated.Geofence
            };
        }
        return null;
    }

    public async Task<List<FieldResponseDto>?> GetFields(long companyId)
    {
        var fields = await _fieldRepository.GetFieldsByCompanyId(companyId);
        if (!fields.Any())
        {
            return new List<FieldResponseDto>();
        }

        return fields.Select(x => new FieldResponseDto()
        {
            Geofence = x.Geofence,
            Id = x.Id,
            Name = x.Name,
            CompanyId = x.CompanyId,
            CompanyFarmId = x.CompanyFarmId
        }).ToList();
    }

    /// <summary>
    /// Get all fields with devices by company id
    /// </summary>
    public async Task<List<FieldResponseDto>> GetFieldsWithDevices(long companyId)
    {
        var fields = await _fieldRepository.GetFieldsByCompanyId(companyId);

        if (!fields.Any())
        {
            return new List<FieldResponseDto>();
        }

        var yesterday = DateTime.Today.AddDays(-1);

        return fields.Select(x => new FieldResponseDto()
        {
            Geofence = x.Geofence,
            Id = x.Id,
            Name = x.Name,
            CompanyId = x.CompanyId,
            CompanyFarmId = x.CompanyFarmId,
            Devices = x.Devices.Select(x => new GetDevicesResponseDto()
            {
                Id = x.Id,
                Name = x.Name,
                // we add the device data from the last 24 hours
                DeviceData = x.DeviceData.Where(data => data.TimeStamp.ToLocalTime().CompareTo(yesterday) > 0).Select(x => new DeviceDataResponseDto()
                {
                    Id = x.Id,
                    DeviceId = x.DeviceId,
                    BatteryLevel = x.BatteryLevel,
                    Sensor1 = x.Sensor1,
                    Sensor2 = x.Sensor2,
                    Temperature = x.Temperature,
                    TimeStamp = x.TimeStamp
                }).ToList()
            }).ToList()

        }).ToList();

    }
}