using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Roles.CreateRole;

public class CreateRoleCommandHandler(IRoleService roleService) 
    : IRequestHandler<CreateRoleCommand,Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleEntity=new Role
        {
         
            RoleName=request.RoleName
        };
        var result = await roleService.CreateRoleAsync(roleEntity, cancellationToken);  
        return result.IsSuccess ? result.Value.ToDto() : result.Errors;
    }
    
}