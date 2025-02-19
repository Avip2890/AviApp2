using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.UserRole.CreateUserRole;

public class CreateUserRoleCommandHandler(IUserRoleService userRoleService) : IRequestHandler<CreateUserRoleCommand,Result<UserRoleDto>>
{
    public async Task<Result<UserRoleDto>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await userRoleService.CreateUserRoleAsync(request.UserId, request.RoleId, cancellationToken);
        return result.IsSuccess ? result.Value.ToDto() : result.Errors;
    }
    
}
