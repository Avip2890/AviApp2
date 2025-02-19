using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.CreateUser;

public record CreateUserCommand(string UserName, string Password, string Email ) : IRequest<Result<UserDto>>;
