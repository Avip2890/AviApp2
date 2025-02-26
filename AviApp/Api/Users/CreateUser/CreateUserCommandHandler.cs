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
        Console.WriteLine($"Creating user: {userRequest.Name}");
        Console.WriteLine($"Requested Roles: {string.Join(", ", userRequest.Name)}");

        var userEntity = new User
        {
            Username = userRequest.Name,
            Password = userRequest.Password,
            Email = userRequest.Email,
            CreatedAt = DateTime.UtcNow,
            Roles = new List<Role>() 
        };
        
        var roles = await context.Roles
            .Where(r => request.RoleNames.Contains(r.RoleName))
            .ToListAsync(cancellationToken);

        Console.WriteLine($"Roles found in DB: {string.Join(", ", roles.Select(r => r.RoleName))}");
        
        foreach (var role in roles)
        {
            if (!userEntity.Roles.Contains(role)) 
            {
                userEntity.Roles.Add(role);
            }
        }

        Console.WriteLine($"Roles assigned to user: {string.Join(", ", userEntity.Roles.Select(r => r.RoleName))}");
        
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

            Console.WriteLine($"User created successfully with Roles: {string.Join(", ", userDto.RoleNames)}");

            return Result<UserDto>.Success(userDto);
        }

        return result.Errors;
    }
}
