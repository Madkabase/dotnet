using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Utilities.Azure;
using IoDit.WebAPI.Utilities.Helpers;

namespace IoDit.WebAPI.Utilities;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services,
        IConfiguration configuration) => services
        .AddSingleton<KeyVaultSecrets>()
        .AddSingleton<IAzureApiClient, AzureApiClient>()
        .AddSingleton<IEmailHelper, EmailHelper>()
        .AddSingleton<IJwtHelper, JwtHelper>()
        .AddSingleton<NotificationsHelper>()
    // reposoitories
        .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
        .AddScoped<IUtilsRepository, UtilsRepository>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IFarmRepository, FarmRepository>()
        .AddScoped<IFarmUserRepository, FarmUserRepository>()
        .AddScoped<IFieldRepository, FieldRepository>()
        .AddScoped<IThresholdPresetRepository, ThresholdPresetRepository>()
        .AddScoped<IThresholdRepository, ThresholdRepository>()
        .AddScoped<IDeviceRepository, DeviceRepository>()

    // services
        .AddScoped<IAuthService, AuthService>()
        .AddScoped<IRefreshJwtService, RefreshJwtService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<IFarmService, FarmService>()
        .AddScoped<IFarmUserService, FarmUserService>()
        .AddScoped<IFieldService, FieldService>()
        .AddScoped<IThresholdPresetService, ThresholdPresetService>()
        .AddScoped<IThresholdService, ThresholdService>()
        .AddScoped<IDeviceService, DeviceService>()
    ;
}