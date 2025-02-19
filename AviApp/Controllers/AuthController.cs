using AviApp.Api.Auth.Login;
using AviApp.Interfaces;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;

public class AuthController(IMediator mediator) : AppBaseController
{

    [HttpPost("/api/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginQuery(loginRequestDto), cancellationToken);
        
        return ResultOf(result);
       
    }
}
