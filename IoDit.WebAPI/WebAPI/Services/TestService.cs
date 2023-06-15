using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Azure;
using IoDit.WebAPI.Utilities.Loriot;
using IoDit.WebAPI.Utilities.Loriot.Types;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Services;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.WebAPI.Services;

public class TestService : ITestService
{
    private readonly IIoDitRepository _repository;
    private readonly LoriotApiClient _loriotApiClient;
    private readonly IAzureApiClient _azureApiClient;
    private readonly IEmailService _emailService;
    private readonly ICompanyUserRepository _companyUserRepository;

    public TestService(IIoDitRepository repository,
        LoriotApiClient loriotApiClient,
        IAzureApiClient azureApiClient,
        IEmailService emailService,
        ICompanyUserRepository companyRepository)
    {
        _repository = repository;
        _loriotApiClient = loriotApiClient;
        _azureApiClient = azureApiClient;
        _emailService = emailService;
        _companyUserRepository = companyRepository;
    }

    public async Task<string> Test()
    {
        await _azureApiClient.RemoveDevice("9F8E7D6C5B4A3B2C");
        var azureDev = await _azureApiClient.CreateDevice("9F8E7D6C5B4A3B2C");
        await _azureApiClient.GetDevices();

        var app = await _loriotApiClient.CreateLoriotApp("viktorzmirnov07", 5);
        await _loriotApiClient.LoriotAppCapacity(app.appHexId, new LoriotAppCapacityRequestDto() { inc = 5 });
        await _loriotApiClient.LoriotAppCapacity(app.appHexId, new LoriotAppCapacityRequestDto() { dec = 4 });
        var apps = await _loriotApiClient.GetLoriotApps();
        var output = await _loriotApiClient.AddLoriotAppOutput(app.appHexId);
        var newDevice = new LoriotCreateAppDeviceRequestDto()
        {
            deveui = "9F8E7D6C5B4A3B2C",
            appkey = "fa7090bc518376de6b745987bbb8f5e9",
            appeui = "9F8E7D6C5B4A3B2C",
            title = "9F8E7D6C5B4A3B2C",
            description = "Device Created by IoDitApp"
        };
        var newDev = await _loriotApiClient.CreateLoriotAppDevice(newDevice, "BE0100A8");

        var devices = await _loriotApiClient.GetLoriotAppDevices(app.appHexId);
        await _loriotApiClient.DeleteLoriotAppDevice(app.appHexId, newDev.deveui);
        await _loriotApiClient.DeleteLoriotApp(app.appHexId);
        return "success";
    }

    public async Task<string> ClearAllDataAsync()
    {
        var dbContext = _repository.DbContext;

        // Remove data from all tables
        dbContext.CompanyUserDeviceData.RemoveRange(dbContext.CompanyUserDeviceData);
        dbContext.CompanyDeviceData.RemoveRange(dbContext.CompanyDeviceData);
        dbContext.CompanyFarmUsers.RemoveRange(dbContext.CompanyFarmUsers);
        dbContext.CompanyFields.RemoveRange(dbContext.CompanyFields);
        dbContext.CompanyFarms.RemoveRange(dbContext.CompanyFarms);
        dbContext.CompanyDevices.RemoveRange(dbContext.CompanyDevices);
        dbContext.CompanyUsers.RemoveRange(dbContext.CompanyUsers);
        dbContext.Companies.RemoveRange(dbContext.Companies);
        dbContext.RefreshTokens.RemoveRange(dbContext.RefreshTokens);
        dbContext.Users.RemoveRange(dbContext.Users);
        await _repository.SaveChangesAsync();
        return "success";
    }

    public async Task<Company?> ClearAllDataAsyncAndPopulate()
    {
        //await ClearAllDataAsync();
        var user = new User()
        {
            Email = "v.zhuk@brainence.com",
            Password = PasswordEncoder.HashPassword("test"),
            IsVerified = true,
            FirstName = "Viktor",
            LastName = "Zhuk",
            AppRole = AppRoles.AppAdmin
        };
        var createdUser = await _repository.CreateAsync(user);

        var loriotApp = await _loriotApiClient.CreateLoriotApp(createdUser.Email, 10);
        var company = new Company()
        {
            Owner = createdUser,
            OwnerId = createdUser.Id,
            MaxDevices = loriotApp.deviceLimit,
            CompanyName = "ViktorTestCompany",
            AppId = loriotApp.appHexId,
            AppName = loriotApp.name
        };
        var createdCompany = await _repository.CreateAsync(company);

        var thresholdPreset = new CompanyThresholdPreset()
        {
            Company = createdCompany,
            CompanyId = createdCompany.Id,
            Name = "Carrot sensor preset",
            DefaultHumidity1Max = 100,
            DefaultHumidity1Min = 10,
            DefaultHumidity2Max = 90,
            DefaultHumidity2Min = 20,
            DefaultTemperatureMax = 50,
            DefaultTemperatureMin = -50,
            DefaultBatteryLevelMax = 100,
            DefaultBatteryLevelMin = 15
        };
        var createdThresholdPreset = await _repository.CreateAsync(thresholdPreset);

        var companyUsers = await _companyUserRepository.GetUserCompanyUsers(createdUser.Email);
        var isDefault = false;
        if (companyUsers?.FirstOrDefault(x => x.IsDefault == true) == null)
        {
            isDefault = true;
        }

        var companyUser = new CompanyUser()
        {
            Company = createdCompany,
            User = createdUser,
            CompanyRole = CompanyRoles.CompanyOwner,
            CompanyId = createdCompany.Id,
            UserId = createdUser.Id,
            IsDefault = isDefault
        };
        var createdCompanyUser = await _repository.CreateAsync(companyUser);

        var companyFarm = new CompanyFarm()
        {
            Company = createdCompany,
            Name = "Viktor Test Farm",
            CompanyId = createdCompany.Id,
        };
        var createdCompanyFarm = await _repository.CreateAsync(companyFarm);

        var companyFarmUser = new CompanyFarmUser()
        {
            Company = createdCompany,
            CompanyFarm = createdCompanyFarm,
            CompanyId = createdCompany.Id,
            CompanyFarmId = createdCompanyFarm.Id,
            CompanyUser = createdCompanyUser,
            CompanyUserId = createdCompanyUser.Id,
            CompanyFarmRole = CompanyFarmRoles.FarmAdmin
        };
        var createdCompanyFarmUser = await _repository.CreateAsync(companyFarmUser);

        var loriotAppDevice = new LoriotCreateAppDeviceRequestDto()
        {
            appkey = "fa7090bc518376de6b745987bbb8f5e9",
            description = "device created by IoDit.WebAPI",
            deveui = "9F8E7D6C5B4A3B2C",
            title = "9F8E7D6C5B4A3B2C",
            appeui = "9F8E7D6C5B4A3B2C"
        };
        var loriotDevice = await _loriotApiClient.CreateLoriotAppDevice(loriotAppDevice, company.AppId);
        var azureDevice = await _azureApiClient.CreateDevice(loriotDevice.deveui);
        var device = new CompanyDevice()
        {
            Farm = createdCompanyFarm,
            FarmId = createdCompanyFarm.Id,
            Company = company,
            CompanyId = company.Id,
            Name = loriotDevice.title,
            AppKey = "fa7090bc518376de6b745987bbb8f5e9",
            DevEUI = loriotDevice.deveui,
            JoinEUI = "9F8E7D6C5B4A3B2C",
            DefaultHumidity1Max = 100,
            DefaultHumidity1Min = 10,
            DefaultHumidity2Max = 90,
            DefaultHumidity2Min = 20,
            DefaultTemperatureMax = 50,
            DefaultTemperatureMin = -50,
            DefaultBatteryLevelMax = 100,
            DefaultBatteryLevelMin = 15,
        };
        var createdDevice = await _repository.CreateAsync(device);

        var coordinates = new[]
        {

            new Coordinate(47.78801194009456, 35.04762707767433),
            new Coordinate(47.77953411993706, 35.048828707313),
            new Coordinate(47.780658808275234, 35.05788384494728),
            new Coordinate(47.788934612593955,  35.05629597721046),
            new Coordinate(47.78801194009456, 35.04762707767433) // Must close the polygon by repeating the first point
        };
        var linearRing = new LinearRing(coordinates);
        var polygon = new Polygon(linearRing);
        var field = new CompanyField()
        {
            Geofence = polygon,
            Name = "test field 1",
            Devices = new List<CompanyDevice>()
            {
                createdDevice
            },
            CompanyId = createdCompany.Id,
            CompanyFarm = createdCompanyFarm,
            CompanyFarmId = createdCompanyFarm.Id,
            Company = createdCompany
        };
        var createdField = await _repository.CreateAsync(field);
        var res = await _repository.DbContext.Companies
            .Include(x => x.Users)
            .ThenInclude(x => x.CompanyFarmUsers)
            .ThenInclude(x => x.CompanyFarm)
            .ThenInclude(x => x.Fields)
            .ThenInclude(x => x.Devices)
            .ThenInclude(x => x.DeviceData)
            .FirstOrDefaultAsync(x => x.OwnerId == 11);
        return res;
    }

    public async Task<string> TestMail()
    {
        var email = new CustomEmailMessage()
        {
            RecipientEmail = "v.zhuk@brainence.com",
            Subject = "API connectivity test",
            Body = "API workds"
        };
        await _emailService.SendEmailWithMailKitAsync(email);
        return "success";
    }
}