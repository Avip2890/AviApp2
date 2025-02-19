using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.DeleteUser;

public class DeleteUserHandler(IUserService userService) : IRequestHandler<DeleteUserCommand,Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.DeleteUserAsync(request.Id, cancellationToken);
        return result.IsSuccess ? result.Value : result.Errors;
    }
    
}