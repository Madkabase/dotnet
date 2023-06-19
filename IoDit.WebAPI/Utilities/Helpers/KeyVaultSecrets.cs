using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace IoDit.WebAPI.Utilities.Helpers;

public class KeyVaultSecrets
{
    private readonly SecretClient client;

    public KeyVaultSecrets(IConfiguration configuration)
    {
        var kvURL = configuration["KeyVaultConfig:KVUrl"];
        var tenantId = configuration["KeyVaultConfig:TenantId"];
        var clientId = configuration["KeyVaultConfig:ClientId"];
        var clientSecret = configuration["KeyVaultConfig:ClientSecret"];
        var credentials = new ClientSecretCredential(tenantId, clientId, clientSecret);

        client = new SecretClient(new Uri(kvURL), credentials);
    }

    public async Task<string> GetSecretAsync(string secretName)
    {
        var secret = await client.GetSecretAsync(secretName);
        return secret.Value.Value;
    }
}