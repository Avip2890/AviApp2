using AviApp.Domain.Entities;
using AviApp.Results;

namespace AviApp.Interfaces;

public interface IUserService
{
    Task<Result<List<User>>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<Result<User>> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<Result<User>> UpdateUserAsync(User updatedUser, CancellationToken cancellationToken);
    Task<Result<Deleted>> DeleteUserAsync(int id, CancellationToken cancellationToken);
    
}