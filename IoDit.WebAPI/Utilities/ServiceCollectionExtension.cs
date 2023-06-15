using IoDit.WebAPI.Utilities.Azure;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Services;
using IoDit.WebAPI.WebAPI.Services;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.Utilities;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services,
        IConfiguration configuration) => services
        .AddSingleton<IKeyVaultSecrets, KeyVaultSecrets>()
        .AddSingleton<IAzureApiClient, AzureApiClient>()
        .AddSingleton<IEmailService, EmailService>()
        // reposoitories
        .AddScoped<IUtilsRepository, UtilsRepository>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<ICompanyRepository, CompanyRepository>()
        .AddScoped<ICompanyUserRepository, CompanyUserRepository>()
        .AddScoped<IFarmRepository, FarmRepository>()
        .AddScoped<IDeviceRepository, DeviceRepository>()
        .AddScoped<IFieldRepository, FieldRepository>()
        .AddScoped<ICompanyFarmUserRepository, CompanyFarmUserRepository>()
        .AddScoped<IThresholdRepository, ThresholdRepository>()

        .AddScoped<IJwtUtils, JwtUtils>()
        // services
        .AddScoped<ITestService, TestService>()
        .AddScoped<IAuthService, AuthService>()
        .AddScoped<ICompanyService, CompanyService>()
        .AddScoped<IDeviceService, DeviceService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<IFarmService, FarmService>()
        .AddScoped<IFieldService, FieldService>()
        .AddScoped<ICompanyUserService, CompanyUserService>()
        .AddScoped<IFarmUserService, FarmUserService>()
        .AddScoped<IThresholdPresetService, ThresholdPresetService>()
        .AddScoped<IDeviceDataService, DeviceDataService>()
        .AddScoped<IUserDeviceDataService, UserDeviceDataService>();
}