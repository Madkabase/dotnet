using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using NLog;
using NLog.Web;

namespace IoDit.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {

        var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (System.Exception e)
        {
            logger.Error(e, "Stopped program because of exception");
            throw;
        }
        finally
        {
            NLog.LogManager.Shutdown();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(ConfigureDelegate)
              .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    private static void ConfigureDelegate(HostBuilderContext context, IConfigurationBuilder config)
    {
        var builtConfiguration = config.Build();

        var kvUrl = builtConfiguration["KeyVaultConfig:KVUrl"];
        var tenantId = builtConfiguration["KeyVaultConfig:TenantId"];
        var clientId = builtConfiguration["KeyVaultConfig:ClientId"];
        var clientSecret = builtConfiguration["KeyVaultConfig:ClientSecret"];

        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

        var client = new SecretClient(new Uri(kvUrl), credential);

        config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

        config.AddEnvironmentVariables()
              .AddUserSecrets<Program>()
              .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    }
}

