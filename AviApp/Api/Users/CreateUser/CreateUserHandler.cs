using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.CreateUser;

public class CreateUserHandler(IUserService userService) 
    : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = new User
        {
            UserName = request.UserName,
            Password = request.Password,
            Email = request.Email
        };

        var result = await userService.CreateUserAsync(userEntity, cancellationToken);

        return result.IsSuccess ? result.Value.ToDto() : result.Errors;
    }
    
}