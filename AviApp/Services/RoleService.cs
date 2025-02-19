using AviApp.Domain.Context;
using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Results;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

public class RoleService(AvipAppDbContext context)  : IRoleService
{
    public async Task<Result<List<Role>>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        var roles = await context.Roles.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        return roles;
    }

    public async Task<Result<Role>> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
    {
        var role = await context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        if (role == null)
        {
            return Error.NotFound("Role not found");
        }

        return role;
    }
    
    public async Task<Result<Role>> CreateRoleAsync(Role role, CancellationToken cancellationToken)
    {
        try
        {
            context.Roles.Add(role);
            
            await context.SaveChangesAsync(cancellationToken); 
            
            return role;
        }
        catch
        {
            return Error.BadRequest("Couldn't create role");
        }
    }

    public async Task<Result<Role>> UpdateRoleAsync(Role updatedRole, CancellationToken cancellationToken)
    {
        var role = await context.Roles.FindAsync(new object[] { updatedRole.Id }, cancellationToken);

        if (role == null)
        {
            return Error.NotFound("Role not found");
        }

        role.RoleName = updatedRole.RoleName;

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return role;
        }
        catch
        {
            return Error.BadRequest("Couldn't update role");
        }
    }

    public async Task<Result<Deleted>> DeleteRoleAsync(int id, CancellationToken cancellationToken)
    {
        var role = await context.Roles.FindAsync(new object[] { id }, cancellationToken);

        if (role == null)
        {
            return Error.NotFound("Role not found");
        }

        context.Roles.Remove(role);

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return new Deleted();
        }
        catch
        {
            return Error.BadRequest("Couldn't delete role");
        }
    }
    
    public async Task<bool> RoleExistsAsync(int roleId, CancellationToken cancellationToken)
    {
        return await context.Roles.AnyAsync(r => r.Id == roleId, cancellationToken);
    }
}