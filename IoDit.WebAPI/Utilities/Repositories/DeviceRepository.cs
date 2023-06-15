using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Utilities.Repositories;

public class DeviceRepository : IDeviceRepository
{

    public IoDitDbContext DbContext { get; }

    public DeviceRepository(IoDitDbContext context)
    {
        DbContext = context;
    }
    public async Task<List<CompanyDevice>> GetDevices(long companyId)
    {
        var company = await DbContext.Companies
            .Include(x => x.Devices)
            .ThenInclude(x => x.DeviceData)
            .FirstOrDefaultAsync(x => x.Id == companyId);

        if (company == null)
        {
            // Handle not finding the company, perhaps return an empty list or throw an exception
            return new List<CompanyDevice>();
        }

        var resultDevices = new List<CompanyDevice>();

        foreach (var device in company.Devices)
        {
            if (!device.DeviceData.Any())
            {
                resultDevices.Add(device);
                continue;
            }

            // Get the latest timestamp for this device
            var latestTimestamp = device.DeviceData.Max(x => x.TimeStamp);

            // Filter the DeviceData for this device
            var filteredDeviceData = device.DeviceData
                .Where(x => x.TimeStamp >= latestTimestamp.AddHours(-1))
                .ToList();

            // Create a new CompanyDevice with the filtered data
            var newDevice = new CompanyDevice
            {
                // Copy all other properties of the device
                Id = device.Id,
                Company = device.Company,
                Farm = device.Farm,
                Field = device.Field,
                Name = device.Name,
                AppKey = device.AppKey,
                CompanyId = device.CompanyId,
                FarmId = device.FarmId,
                FieldId = device.FieldId,
                DefaultHumidity1Max = device.DefaultHumidity1Max,
                DefaultHumidity1Min = device.DefaultHumidity1Min,
                DefaultHumidity2Max = device.DefaultHumidity2Max,
                DefaultHumidity2Min = device.DefaultHumidity2Min,
                DefaultTemperatureMax = device.DefaultTemperatureMax,
                DefaultTemperatureMin = device.DefaultTemperatureMin,
                DefaultBatteryLevelMax = device.DefaultBatteryLevelMax,
                DefaultBatteryLevelMin = device.DefaultBatteryLevelMin,
                DevEUI = device.DevEUI,
                JoinEUI = device.JoinEUI,
                DeviceData = filteredDeviceData
            };

            resultDevices.Add(newDevice);
        }

        return resultDevices;
    }

    public async Task<CompanyDevice?> GetDeviceByEui(string deviceEUI) =>
        await Task.Run(() => DbContext.CompanyDevices.FirstOrDefault(x => x.DevEUI == deviceEUI));

    //*******************************************************************************************
    //DEVICE DATA
    public async Task<CompanyDevice?> GetDeviceWithDataByEui(string deviceEUI) =>
        await Task.Run(() =>
            DbContext.CompanyDevices.Include(x => x.DeviceData).FirstOrDefault(x => x.DevEUI == deviceEUI));

}