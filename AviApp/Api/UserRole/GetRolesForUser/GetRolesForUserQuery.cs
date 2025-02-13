using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.UserRole.GetRolesForUser;

public record GetRolesForUserQuery(int UserId) : IRequest<Result<List<RoleDto>>>;
