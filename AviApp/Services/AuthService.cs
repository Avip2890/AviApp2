using AviApp.Domain.Context;
using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Results;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

public class AuthService(AvipAppDbContext context, JwtService jwtService) : IAuthService
{
    public async Task<Result<string>> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await context.Users.Include(x => x.Roles).FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        if (user == null)
        {
            return Error.NotFound("User not found");
        }

        if (user.Password != password)
        {
            return Error.BadRequest("Invalid password");
        }

        var token = jwtService.GenerateToken(user);  

        return token;
    }
}
