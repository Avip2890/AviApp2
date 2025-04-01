using AviApp.Api.Auth.Login;
using AviApp.Attributes;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AviApp.Controllers;

public class AuthController(IMediator mediator) : AppBaseController
{
    /// <summary>
    /// Login
    /// </summary>
    /// <remarks>Login to the application</remarks>
    /// <param name="loginRequestDto"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpPost]
    [Route("/api/login")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("Login")]
    [SwaggerResponse(statusCode: 200, type: typeof(string), description: "OK")]
    public virtual async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginQuery(loginRequestDto), cancellationToken);
        return ResultOf(result);
       
    }
    
}
