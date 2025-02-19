using AviApp.Results;
using MediatR;

namespace AviApp.Api.UserRole.DeleteUserRole;

public record DeleteUserRoleCommand(int UserId, int RoleId) : IRequest<Result<Deleted>>;
