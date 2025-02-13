using AviApp.Domain.Entities;
using AviApp.Models;

namespace AviApp.Mappers
{
    public static class UserMapper
    {
        public static User ToEntity(this UserDto model)
        {
            return new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                CreatedAt = model.CreatedAt
            };
        }

        public static UserDto ToDto(this User entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Password = entity.Password,
                Email = entity.Email,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}