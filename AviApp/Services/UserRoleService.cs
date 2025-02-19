using AviApp.Domain.Context;
using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using Microsoft.EntityFrameworkCore;


namespace AviApp.Services;

public class UserRoleService(AvipAppDbContext context) : IUserRoleService
{
    public async Task<Result<UserRole>> CreateUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(userId, cancellationToken);
        var role = await context.Roles.FindAsync(roleId, cancellationToken);
        
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        

        

        context.Roles.Add(role);
        await context.SaveChangesAsync(cancellationToken);

        return new UserRole
        {
            UserId = userId,
            RoleId = roleId,
            User = user,
            Role = role
        };
    }

    public async Task<Result<List<RoleDto>>> GetRolesForUserAsync(int userId, CancellationToken cancellationToken)
    {
        var roles = await context.Users
            .Where(ur => ur.Id == userId)
            .Select(ur => ur.Roles)
            .FirstOrDefaultAsync(cancellationToken);

        var roleDtos = roles?.Select(r => new RoleDto
        {
            Id = r.Id,
            RoleName = r.RoleName
        }).ToList();
        
        return Result<List<RoleDto>>.Success(roleDtos ?? new List<RoleDto>());
    }

    public async Task<Result<List<UserDto>>> GetUsersByRoleAsync(int roleId, CancellationToken cancellationToken)
    {
        var users = await context.Roles
            .Where(ur => ur.Id == roleId)
            .Select(ur => ur.Users)
            .FirstOrDefaultAsync(cancellationToken);

        var userDtos = users?.Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.Username,
            Password = u.Password,
            Email = u.Email
        }).ToList();

        return Result<List<UserDto>>.Success(userDtos ?? new List<UserDto>());
    }

    public async Task<Result<Deleted>> DeleteUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
    {
        var userRole = await context.Users
            .FirstOrDefaultAsync(ur => ur.Id == userId && ur.Roles.Any(r => r.Id == roleId), cancellationToken);

        if (userRole == null)
        {
            return Error.NotFound("UserRole not found.");
        }

        context.Users.Remove(userRole);
        await context.SaveChangesAsync(cancellationToken);

        return new Deleted();
    }
}