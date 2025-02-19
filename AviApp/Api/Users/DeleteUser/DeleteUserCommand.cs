using AviApp.Results;
using MediatR;

namespace AviApp.Api.Users.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<Result<Deleted>>;
