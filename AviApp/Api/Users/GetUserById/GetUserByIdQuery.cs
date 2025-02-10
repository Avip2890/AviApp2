using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.GetUserById;

public record GetUserByIdQuery (int Id) : IRequest<Result<UserDto>>;
