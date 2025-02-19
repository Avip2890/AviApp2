using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.GetUserById;

public class GetUserByIdHandler(IUserService userService) : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await userService.GetUserByIdAsync(request.Id, cancellationToken);
        return result.IsSuccess ? result.Value.ToDto() : result.Errors;
    }
    
}