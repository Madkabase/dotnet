using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.ThresholdPreset;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class ThresholdPresetService : IThresholdPresetService
{
    private readonly IIoDitRepository _repository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IThresholdRepository _thresholdRepository;

    public ThresholdPresetService(IIoDitRepository repository,
        ICompanyRepository companyRepository,
        IThresholdRepository thresholdRepository)
    {
        _repository = repository;
        _companyRepository = companyRepository;
        _thresholdRepository = thresholdRepository;
    }

    public async Task<GetThresholdPresetsResponseDto> CreateThresholdPreset(CreateThresholdPreset request)
    {
        var company = await _companyRepository.GetCompanyById(request.CompanyId);
        if (company == null)
        {
            return null;
        }
        var companyThresholdPreset = new CompanyThresholdPreset()
        {
            CompanyId = company.Id,
            Company = company,
            Name = request.Name,
            DefaultHumidity1Max = request.DefaultHumidity1Max,
            DefaultHumidity1Min = request.DefaultHumidity1Min,
            DefaultHumidity2Max = request.DefaultHumidity2Max,
            DefaultHumidity2Min = request.DefaultHumidity2Min,
            DefaultTemperatureMax = request.DefaultTemperatureMax,
            DefaultTemperatureMin = request.DefaultTemperatureMin,
            DefaultBatteryLevelMax = request.DefaultBatteryLevelMax,
            DefaultBatteryLevelMin = request.DefaultBatteryLevelMin
        };
        var createdThresholdPreset = await _repository.CreateAsync(companyThresholdPreset);


        return new GetThresholdPresetsResponseDto()
        {
            Id = createdThresholdPreset.Id,
            Name = createdThresholdPreset.Name,
            CompanyId = createdThresholdPreset.CompanyId,
            DefaultHumidity1Max = createdThresholdPreset.DefaultHumidity1Max,
            DefaultHumidity1Min = createdThresholdPreset.DefaultHumidity1Min,
            DefaultHumidity2Max = createdThresholdPreset.DefaultHumidity2Max,
            DefaultHumidity2Min = createdThresholdPreset.DefaultHumidity2Min,
            DefaultTemperatureMax = createdThresholdPreset.DefaultTemperatureMax,
            DefaultTemperatureMin = createdThresholdPreset.DefaultTemperatureMin,
            DefaultBatteryLevelMax = createdThresholdPreset.DefaultBatteryLevelMax,
            DefaultBatteryLevelMin = createdThresholdPreset.DefaultBatteryLevelMin
        };
    }

    public async Task<List<GetThresholdPresetsResponseDto>?> GetThresholdPresets(long companyId)
    {
        var presets = await _thresholdRepository.GetCompanyThresholdPresetsByCompanyId(companyId);
        if (!presets.Any())
        {
            return new List<GetThresholdPresetsResponseDto>();
        }

        return presets.Select(x => new GetThresholdPresetsResponseDto()
        {
            Id = x.Id,
            Name = x.Name,
            CompanyId = x.CompanyId,
            DefaultHumidity1Max = x.DefaultHumidity1Max,
            DefaultHumidity1Min = x.DefaultHumidity1Min,
            DefaultHumidity2Max = x.DefaultHumidity2Max,
            DefaultHumidity2Min = x.DefaultHumidity2Min,
            DefaultTemperatureMax = x.DefaultTemperatureMax,
            DefaultTemperatureMin = x.DefaultTemperatureMin,
            DefaultBatteryLevelMax = x.DefaultBatteryLevelMax,
            DefaultBatteryLevelMin = x.DefaultBatteryLevelMin
        }).ToList();
    }
}