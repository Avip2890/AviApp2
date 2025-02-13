using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.CreateRole;

public record CreateRoleCommand(string RoleName) : IRequest<Result<RoleDto>>;
