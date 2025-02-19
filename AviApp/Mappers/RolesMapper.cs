using AviApp.Domain.Entities;
using AviApp.Models;

namespace AviApp.Mappers;

public static class RolesMapper
{
    public static Role ToEntity(this RoleDto model)
    {
        return new Role
        {
            RoleName = model.RoleName
        };
    }
    
    public static RoleDto ToDto(this Role entity)
    {
        return new RoleDto
        {
            Id = entity.Id,
            RoleName = entity.RoleName
        };
    }
    
}