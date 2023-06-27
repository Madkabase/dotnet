using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IoDit.WebAPI.Utilities.Helpers;

public class JwtHelper : IJwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(string email)
    {
        var claims = new[]
        {
            new Claim("sub", email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var secret = _configuration["JwtSettings-SecretKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddDays(30);
        var tokenDesc = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = credentials,
            Audience = "http://localhost:8100",
            Issuer = "https://localhost:7161"
        };

        var sToken = new JwtSecurityTokenHandler().CreateToken(tokenDesc);

        return new JwtSecurityTokenHandler().WriteToken(sToken);
    }
}