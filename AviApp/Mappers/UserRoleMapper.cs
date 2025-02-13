using AviApp.Domain.Entities;
using AviApp.Models;

namespace AviApp.Mappers;

    public static class UserRoleMapper
    {
        public static UserRole ToEntity(this UserRoleDto userRoleDto)
        {
            return new UserRole
            {
                UserId = userRoleDto.UserId,
                RoleId = userRoleDto.RoleId,
                User = userRoleDto.User.ToEntity(), 
                Role = userRoleDto.Role.ToEntity()  
            };
        }
        
        public static UserRoleDto ToDto(this UserRole userRole)
        {
            return new UserRoleDto
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                User = userRole.User.ToDto(),  
                Role = userRole.Role.ToDto()   
            };
        }
    }
