using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Utilities;

public interface IJwtUtils
{
    public string GenerateJwtToken(string email);
    Task<RefreshToken?> GenerateRefreshToken(string email, string deviceIdentifier);
}