namespace IoDit.WebAPI.Utilities.Helpers
{
    public interface IJwtHelper
    {
        string GenerateJwtToken(string email);
    }
}