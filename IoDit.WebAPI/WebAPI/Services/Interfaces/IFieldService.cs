namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

using IoDit.WebAPI.WebAPI.Models.Field;

public interface IFieldService
{
    Task<FieldResponseDto> CreateCompanyField(CreateCompanyField request);
    Task<FieldResponseDto> UpdateGeofence(UpdateGeofence request);
    Task<List<FieldResponseDto>?> GetFields(long companyUserId);
    Task<List<FieldResponseDto>> GetFieldsWithDevices(long companyId);
}