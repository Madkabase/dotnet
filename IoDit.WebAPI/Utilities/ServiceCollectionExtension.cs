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
        .AddScoped<IIoDitRepository, IoDitRepository>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<ICompanyRepository, CompanyRepository>()
        .AddScoped<IJwtUtils, JwtUtils>()
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