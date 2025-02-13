using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.UserRole.GetUsersByRole;

public record GetUsersByRoleQuery(int RoleId) : IRequest<Result<List<UserDto>>>;