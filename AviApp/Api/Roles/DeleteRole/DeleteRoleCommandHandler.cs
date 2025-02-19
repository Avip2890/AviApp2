using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.DeleteRole;

public class DeleteRoleCommandHandler(IRoleService roleService) : IRequestHandler<DeleteRoleCommand,Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await roleService.DeleteRoleAsync(request.Id, cancellationToken);
        return result.IsSuccess ? result.Value : result.Errors;
    }
    
}
