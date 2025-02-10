using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.GetAllUsers;

public class GetAllUsersHandler(IUserService userService): IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
{
    public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await userService.GetAllUsersAsync(cancellationToken);
        return result.IsSuccess
            ? result.Value.Select(u => u.ToDto()).ToList()
            : Error.BadRequest("User service returned an empty result");
    }
}
