using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.UpdateUser;

public record UpdateUserCommand(int Id,string UserName,string Email,string Password): IRequest<Result<UserDto>>;
