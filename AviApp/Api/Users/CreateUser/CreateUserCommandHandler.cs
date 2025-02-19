using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.CreateUser;

public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = new User
        {
            Username = request.UserName,
            Password = request.Password,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow 
        };

        var result = await userService.CreateUserAsync(userEntity, cancellationToken);

        if (result.IsSuccess)
        {
            var userDto = result.Value.ToDto(); 
            return Result<UserDto>.Success(userDto);
        }

        return result.Errors;
    }
}
