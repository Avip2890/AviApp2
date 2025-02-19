using AviApp.Domain.Entities;
using AviApp.Models;
using AviApp.Results;

namespace AviApp.Interfaces;

public interface IUserRoleService
{
    Task<Result<List<RoleDto>>> GetRolesForUserAsync(int userId, CancellationToken cancellationToken);
    Task<Result<List<UserDto>>> GetUsersByRoleAsync(int roleId, CancellationToken cancellationToken);
    Task<Result<UserRole>> CreateUserRoleAsync(int userId, int roleId ,CancellationToken cancellationToken); 
    Task<Result<Deleted>> DeleteUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken);
    
     
}