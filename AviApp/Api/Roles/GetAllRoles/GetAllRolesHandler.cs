using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.GetAllRoles;

public class GetAllRolesHandler(IRoleService roleService) :IRequestHandler<GetAllRolesQuery, Result<List<RoleDto>>>
{
    public async Task<Result<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var result = await roleService.GetAllRolesAsync(cancellationToken);
        return result.IsSuccess
        
            ? result.Value.Select(r => r.ToDto()).ToList()
            : Error.BadRequest("Role service returned an empty result");
    }
    
}