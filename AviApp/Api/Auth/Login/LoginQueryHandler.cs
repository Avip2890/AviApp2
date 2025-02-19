using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Auth.Login;


public class LoginQueryHandler(IAuthService authService) : IRequestHandler<LoginQuery, Result<string>>
{
    public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request.LoginRequestDto.Email, request.LoginRequestDto.Password, cancellationToken);
    }
}