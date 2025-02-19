using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AviApp.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AviApp.Services;

public class JwtService(string secretKey)
{
    private readonly string _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey), "Secret Key cannot be null");

    public string GenerateToken(User user)
    {
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new List<Claim>
        
        {
            new Claim(nameof(user.Id), user.Id.ToString()),
            new Claim(nameof(user.Email), user.Email),
            new Claim(ClaimTypes.Role, (user?.Roles?.Select(ur => ur.RoleName).FirstOrDefault()) ?? string.Empty)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var sectoken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: creds);

        var token =  new JwtSecurityTokenHandler().WriteToken(sectoken);
        return token;
    }
}
