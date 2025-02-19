using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.GetAllRoles;


public record GetAllRolesQuery() : IRequest<Result<List<RoleDto>>>;
