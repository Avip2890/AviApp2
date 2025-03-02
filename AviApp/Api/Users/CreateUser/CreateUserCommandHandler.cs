using AviApp.Domain.Context;
using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Api.Users.CreateUser;

public class CreateUserCommandHandler(IUserService userService, AvipAppDbContext context) 
    : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userRequest = request.CreateUserRequest;
        
        var userEntity = new User
        {
            Username = userRequest.Name,
            Password = userRequest.Password,
            Email = userRequest.Email,
            CreatedAt = DateTime.UtcNow,
            Roles = new List<Role>() 
        };
        
        var roles = await context.Roles
            .Where(r => request.CreateUserRequest.Roles.Select(roleDto => roleDto.Id).Contains(r.Id))
            .ToListAsync(cancellationToken);
        
        foreach (var role in roles)
        {
            if (!userEntity.Roles.Contains(role)) 
            {
                userEntity.Roles.Add(role);
            }
        }
        
        var result = await userService.CreateUserAsync(userEntity, cancellationToken);

        if (result.IsSuccess)
        {
           
            var savedUser = await context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userEntity.Id, cancellationToken);

            if (savedUser == null)
            {
                return Error.NotFound("Failed to retrieve saved user.");
            }

            var userDto = savedUser.ToDto();
            
            return Result<UserDto>.Success(userDto);
        }

        return result.Errors;
    }
}
