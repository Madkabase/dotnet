namespace IoDit.WebAPI.Utilities.Services;

public interface IKeyVaultSecrets
{
    Task<string> GetSecretAsync(string secretName);
}