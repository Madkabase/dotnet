using IoDit.WebAPI.WebAPI.Models.Field;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface IFieldService
{
    Task<FieldResponseDto> CreateCompanyField(CreateCompanyField request);
    Task<FieldResponseDto> UpdateGeofence(UpdateGeofence request);
    Task<List<FieldResponseDto>?> GetFields(long companyUserId);
}