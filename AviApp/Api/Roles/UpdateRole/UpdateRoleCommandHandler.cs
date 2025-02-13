using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.UpdateRole;

public class UpdateRoleCommandHandler(IRoleService roleService) :IRequestHandler<UpdateRoleCommand,Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRoleResult = await roleService.GetRoleByIdAsync(request.Id, cancellationToken);
        if (!existingRoleResult.IsSuccess)
        {
            return Error.NotFound("Role with ID {request.Id} not found.");
        }
        var existingRole = existingRoleResult.Value;
        existingRole.Id = request.Id;
        existingRole.RoleName = request.RoleName;
        
        var updatedRoleResult = await roleService.UpdateRoleAsync(existingRole, cancellationToken);
        return (updatedRoleResult).IsSuccess ? updatedRoleResult.Value.ToDto() : Error.BadRequest("Update Failed");
        
       
    }
    
}