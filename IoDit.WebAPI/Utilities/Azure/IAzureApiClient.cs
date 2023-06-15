using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;

namespace IoDit.WebAPI.Utilities.Azure;

public interface IAzureApiClient
{
    public Task<Device> CreateDevice(string devId);
    public Task<Device> RemoveDevice(string devId);
    public Task<List<Twin>> GetDevices();
}