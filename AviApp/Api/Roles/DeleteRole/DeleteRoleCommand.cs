using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.DeleteRole;

public record DeleteRoleCommand(int Id) : IRequest<Result<Deleted>>;
