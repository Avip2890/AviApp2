using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.GetRoleById;

public class GetRoleByIdHandler(IRoleService roleService)
    : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>

{
    public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await roleService.GetRoleByIdAsync(request.Id, cancellationToken);
        return result.IsSuccess ? result.Value.ToDto() : result.Errors;
    }
}