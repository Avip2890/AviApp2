using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Auth.Login;

public record LoginQuery(LoginRequestDto LoginRequestDto) : IRequest<Result<string>>;