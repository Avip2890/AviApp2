using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.CreateUser;

public record CreateUserCommand(UserDto CreateUserRequest) : IRequest<Result<UserDto>>;