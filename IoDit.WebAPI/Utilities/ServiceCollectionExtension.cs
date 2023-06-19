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
        .AddSingleton<EmailService>()
        .AddSingleton<JwtHelper>()
    // reposoitories
        .AddScoped<RefreshTokenRepository>()
        .AddScoped<UtilsRepository>()
        .AddScoped<UserRepository>()
    // services
    .AddScoped<AuthService>()
    .AddScoped<RefreshJwtService>()
    .AddScoped<UserService>();
}