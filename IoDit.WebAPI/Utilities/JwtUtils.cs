using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace IoDit.WebAPI.Utilities;

public class JwtUtils : IJwtUtils
{
    private readonly IIoDitRepository _repository;

    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public JwtUtils(IIoDitRepository repository, IUserRepository userRepository, IConfiguration configuration)
    {
        _repository = repository;
        _userRepository = userRepository;
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

    public async Task<RefreshToken?> GenerateRefreshToken(string email, string deviceIdentifier)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user == null)
        {
            return null;
        }

        var userTokens = await _userRepository.GetRefreshTokensForUser(user.Id);
        var expiredTokens = userTokens.Where(t => t.Expires < DateTime.UtcNow).ToList();
        if (expiredTokens.Any())
        {
            await _repository.DeleteRangeAsync(expiredTokens);
        }
        var currentToken = userTokens.FirstOrDefault(x => x.DeviceIdentifier == deviceIdentifier);

        bool isTokenUnique;
        string newToken;

        do
        {
            newToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            isTokenUnique = await _userRepository.CheckIfRefreshTokenExist(newToken);
        }
        while (!isTokenUnique);

        if (currentToken != null)
        {
            currentToken.Token = newToken;
            currentToken.Expires = DateTime.UtcNow.AddDays(7);
            await _repository.UpdateAsync(currentToken);
            return currentToken;
        }

        var refreshToken = new RefreshToken()
        {
            Token = newToken,
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
            DeviceIdentifier = deviceIdentifier,
            User = user,
        };

        await _repository.CreateAsync(refreshToken);
        return refreshToken;
    }
}