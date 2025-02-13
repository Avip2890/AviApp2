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
        
        var userRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId,
            User = user,
            Role = role
        };

        context.UserRoles.Add(userRole);
        await context.SaveChangesAsync(cancellationToken);

        return Result<UserRole>.Success(userRole);
    }

    public async Task<Result<List<RoleDto>>> GetRolesForUserAsync(int userId, CancellationToken cancellationToken)
    {
        var roles = await context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role)
            .ToListAsync(cancellationToken);

        var roleDtos = roles.Select(r => new RoleDto
        {
            Id = r.Id,
            RoleName = r.RoleName
        }).ToList();

        return Result<List<RoleDto>>.Success(roleDtos);
    }

    public async Task<Result<List<UserDto>>> GetUsersByRoleAsync(int roleId, CancellationToken cancellationToken)
    {
        var users = await context.UserRoles
            .Where(ur => ur.RoleId == roleId)
            .Select(ur => ur.User)
            .ToListAsync(cancellationToken);

        var userDtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            Password = u.Password,
            Email = u.Email
        }).ToList();

        return Result<List<UserDto>>.Success(userDtos);
    }

    public async Task<Result<Deleted>> DeleteUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
    {
        var userRole = await context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

        if (userRole == null)
        {
            return Error.NotFound("UserRole not found.");
        }

        context.UserRoles.Remove(userRole);
        await context.SaveChangesAsync(cancellationToken);

        return new Deleted();
    }
}