using AviApp.Domain.Entities;
using AviApp.Models;
using AviApp.Results;

namespace AviApp.Interfaces;

public interface IRoleService
{
    Task<Result<List<Role>>> GetAllRolesAsync(CancellationToken cancellationToken);
    Task<Result<Role>> GetRoleByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<Role>> CreateRoleAsync(Role role, CancellationToken cancellationToken);
    Task<Result<Role>> UpdateRoleAsync(Role updatedRole, CancellationToken cancellationToken);
    Task<Result<Deleted>> DeleteRoleAsync(int id, CancellationToken cancellationToken);
    

    // פונקציה עבור בדיקה אם תפקיד קיים
    Task<bool> RoleExistsAsync(int roleId, CancellationToken cancellationToken);
}