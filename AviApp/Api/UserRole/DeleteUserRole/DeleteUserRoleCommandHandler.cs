using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.UserRole.DeleteUserRole;

public class DeleteUserRoleCommandHandler(IUserRoleService userRoleService):IRequestHandler<DeleteUserRoleCommand,Result<Deleted>> 
{
    public async Task<Result<Deleted>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await userRoleService.DeleteUserRoleAsync(request.UserId, request.RoleId, cancellationToken);
        return result.IsSuccess ? result.Value : result.Errors;
    }
}