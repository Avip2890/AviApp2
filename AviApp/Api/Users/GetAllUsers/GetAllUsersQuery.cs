using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.GetAllUsers;


public record GetAllUsersQuery() : IRequest<Result<List<UserDto>>>;