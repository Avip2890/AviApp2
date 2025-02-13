using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.GetRoleById;

public record GetRoleByIdQuery(int Id) : IRequest<Result<RoleDto>>;
