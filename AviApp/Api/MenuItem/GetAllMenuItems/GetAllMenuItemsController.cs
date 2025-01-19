using AviApp.Api.MenuItem.MenuItemQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.MenuItem.MenuItemHandlers.GetAllMenuItems;

[ApiController]
[Route("api/menuitem/all")]
public class GetAllMenuItemsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllMenuItemsQuery(), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return Ok(result.Value);
    }
}