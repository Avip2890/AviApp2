using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;

public class ErrorControllers: ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    
    [Route("/error")]
    
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem();
    }
    
}