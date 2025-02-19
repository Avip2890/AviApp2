using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.UpdateRole;

public record UpdateRoleCommand(int Id, string RoleName) : IRequest<Result<RoleDto>>;
