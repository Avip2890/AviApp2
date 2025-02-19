using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.UserRole.CreateUserRole;

public record CreateUserRoleCommand(int UserId, int RoleId) : IRequest<Result<UserRoleDto>>;
