using IoDit.WebAPI.WebAPI.Models.Device;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface IDeviceService
{
    Task<GetDevicesResponseDto> CreateDevice(CreateDeviceRequestDto request);
    Task<List<GetDevicesResponseDto>?> GetDevices(long companyId);
    Task<GetDevicesResponseDto?> AssignToField(AssignToFieldRequestDto dto);
}