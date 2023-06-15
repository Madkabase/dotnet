using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.Field;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class FieldService : IFieldService
{
    private readonly IIoDitRepository _repository;

    public FieldService(IIoDitRepository repository)
    {
        _repository = repository;
    }

    public async Task<FieldResponseDto> CreateCompanyField(CreateCompanyField request)
    {
        var company = await _repository.GetCompanyById(request.CompanyId);
        var companyFarm = await _repository.GetCompanyFarmById(request.CompanyFarmId);
        
        var companyField = new CompanyField()
        {
            Company = company,
            Geofence = request.Geofence,
            Name = request.Name,
            CompanyFarm = companyFarm,
            CompanyId = request.CompanyId,
            CompanyFarmId = request.CompanyFarmId
        };
        var createdField = await _repository.CreateAsync(companyField);
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
        var companyField = await _repository.GetCompanyFieldById(request.FieldId);
        if (companyField != null)
        {
            companyField.Geofence = request.Geofence;
            var updated = await _repository.UpdateAsync(companyField);
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
        var fields = await _repository.GetCompanyFields(companyId);
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
}