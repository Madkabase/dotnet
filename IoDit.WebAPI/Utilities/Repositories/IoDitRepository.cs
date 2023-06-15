using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Utilities.Repositories;

public class IoDitRepository : IIoDitRepository
{
    public IoDitDbContext DbContext { get; }

    public IoDitRepository(IoDitDbContext context)
    {
        DbContext = context;
    }

    //*******************************************************************************************
    //DEVICES
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

    //*******************************************************************************************
    //FARM
    public async Task<IQueryable<CompanyFarm>> GetCompanyFarms(long companyId) =>
        await Task.Run(() =>
            DbContext.Companies.Include(x => x.Farms).Where(x => x.Id == companyId).SelectMany(x => x.Farms));

    public async Task<CompanyUser?> GetCompanyUserFarms(long companyUserId) =>
        await Task.Run(() => DbContext.CompanyUsers
            .Include(x => x.CompanyFarmUsers).ThenInclude(x => x.CompanyFarm)
            .FirstOrDefault(x => x.Id == companyUserId));

    public async Task<CompanyFarm?> GetCompanyFarmById(long companyFarmId) =>
        await Task.Run(() => DbContext.CompanyFarms.FirstOrDefault(x => x.Id == companyFarmId));

    //*******************************************************************************************
    //FIELD 
    public async Task<IQueryable<CompanyField>> GetCompanyFields(long companyId) =>
        await Task.Run(() =>
            DbContext.Companies.Include(x => x.Fields).Where(x => x.Id == companyId).SelectMany(x => x.Fields));

    public async Task<CompanyField?> GetCompanyFieldById(long companyFieldId) =>
        await Task.Run(() =>
            DbContext.CompanyFields.FirstOrDefault(x => x.Id == companyFieldId));

    //*******************************************************************************************
    //COMPANY FARM USERS
    public async Task<IQueryable<CompanyFarmUser>> GetCompanyFarmUsers(long companyId) =>
        await Task.Run(() =>
            DbContext.Companies.Include(x => x.FarmUsers).Where(x => x.Id == companyId).SelectMany(x => x.FarmUsers));

    public async Task<IQueryable<CompanyFarmUser>> GetCompanyUserFarmUsers(long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyFarmUsers.Where(x => x.CompanyUserId == companyUserId));

    public async Task<CompanyFarmUser?> GetCompanyUserFarmUser(long companyFarmId, long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyFarmUsers.FirstOrDefault(x =>
                x.CompanyUserId == companyUserId && x.CompanyFarmId == companyFarmId));

    //*******************************************************************************************
    //THRESHOLD PRESETS
    public async Task<IQueryable<CompanyThresholdPreset>> GetCompanyThresholdPresetsByCompanyId(long companyId) =>
        await Task.Run(() =>
            DbContext.CompanyThresholdPreset.Where(x => x.CompanyId == companyId));

    //*******************************************************************************************
    //APP
    public async Task<IQueryable<CompanyUserDeviceData>> GetCompanyUserThresholds(long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyUsers.Include(x => x.CompanyUserDeviceData).Where(x => x.Id == companyUserId)
                .SelectMany(x => x.CompanyUserDeviceData));

    //*******************************************************************************************
    //APP
    public async Task<RefreshToken?> GetRefreshToken(string token) =>
        await Task.Run(() => DbContext.RefreshTokens.FirstOrDefault(x => x.Token == token));

    public async Task<bool> CheckIfRefreshTokenExist(string token) =>
        await Task.Run(() => DbContext.RefreshTokens.FirstOrDefault(x => x.Token == token) == null);

    public async Task<IQueryable<RefreshToken>> GetRefreshTokensForUser(long userId) =>
        await Task.Run(() => DbContext.RefreshTokens.Select(x => x).Where(x => x.UserId == userId));

    //*******************************************************************************************
    //REPO UTILS
    public async Task<T> CreateAsync<T>(T entity) where T : class, IEntity
    {
        DbContext.Add(entity);
        await SaveChangesAsync();

        return entity;
    }

    public async Task<T> UpdateAsync<T>(T entity) where T : class, IEntity
    {
        DbContext.Update(entity);
        await SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync<T>(T entity) where T : class, IEntity
    {
        DbContext.Remove(entity);
        await SaveChangesAsync();
    }

    public async Task<List<T>> CreateRangeAsync<T>(List<T> entities) where T : class, IEntity
    {
        DbContext.AddRange(entities);
        await SaveChangesAsync();

        return entities;
    }

    public async Task<List<T>> UpdateRangeAsync<T>(List<T> entities) where T : class, IEntity
    {
        DbContext.UpdateRange(entities);
        await SaveChangesAsync();

        return entities;
    }

    public async Task DeleteRangeAsync<T>(List<T> entities) where T : class, IEntity
    {
        DbContext.RemoveRange(entities);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}