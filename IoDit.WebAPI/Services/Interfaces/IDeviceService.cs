using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Services;

public interface IDeviceService
{
    public Task<Device> CreateDevice(CreateDeviceRequestDto createDeviceRequestDto);
    Task<Device> GetDeviceByDevEUI(string devEUI);
}