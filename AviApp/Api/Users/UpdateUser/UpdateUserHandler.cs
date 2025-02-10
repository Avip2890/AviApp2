using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.UpdateUser;

public class UpdateUserHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUserResult = await userService.GetUserByIdAsync(request.Id, cancellationToken);
        
        if (!existingUserResult.IsSuccess)
        {
            return Error.NotFound($"User With ID {request.Id} found");
        }
        
        var existingUser = existingUserResult.Value;
        
        existingUser.UserName = request.UserName;
        existingUser.Email = request.Email;
        existingUser.Password = request.Password;
        
        var updatedUserResult = await userService.UpdateUserAsync(existingUser, cancellationToken);
        
        return (updatedUserResult.IsSuccess) ? updatedUserResult.Value.ToDto() : Error.BadRequest("Update Failed");
    }
}