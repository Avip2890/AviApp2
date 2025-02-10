using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AviApp.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AviApp.Services;

public class JwtService(string secretKey)
{
    public string GenerateJwtToken(User user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

 
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:5099",  
            audience: "http://localhost:5099", 
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
