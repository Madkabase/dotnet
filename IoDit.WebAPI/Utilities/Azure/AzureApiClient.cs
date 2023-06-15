using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;

namespace IoDit.WebAPI.Utilities.Azure;

public class AzureApiClient: IAzureApiClient
{
    private readonly IConfiguration _configuration;

    public AzureApiClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Device> CreateDevice(string devId)
    {
        var connectionString = _configuration["IoTHubSettings-RegistryRWConnectionString"];
        using var deviceClient = RegistryManager.CreateFromConnectionString(connectionString);
        try
        {
            await deviceClient.OpenAsync();
            var device = await deviceClient.AddDeviceAsync(new Device(devId));
            await deviceClient.CloseAsync();
            return device;
        }
        catch (Exception e)
        {
            await deviceClient.CloseAsync();
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<Device> RemoveDevice(string devId)
    {
        var connectionString = _configuration["IoTHubSettings-RegistryRWConnectionString"];
        using var deviceClient = RegistryManager.CreateFromConnectionString(connectionString);
        try
        {
            await deviceClient.RemoveDeviceAsync(devId);
            var deletedDevice = await deviceClient.GetDeviceAsync(devId);//should be null if deleted
            return deletedDevice;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<List<Twin>> GetDevices()
    {
        var devices = new List<Twin>();
        var connectionString = _configuration["IoTHubSettings-RegistryRWConnectionString"];
        using var deviceClient = RegistryManager.CreateFromConnectionString(connectionString);
        var query = deviceClient.CreateQuery("select * from devices", 100);
        while (query.HasMoreResults)
        {
            var batch = await query.GetNextAsTwinAsync();
            devices.AddRange(batch);
        }
        return devices;

    }
    
}