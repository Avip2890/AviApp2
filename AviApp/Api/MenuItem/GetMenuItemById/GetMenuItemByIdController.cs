using AviApp.Api.MenuItem.MenuItemQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.MenuItem.MenuItemHandlers.GetMenuItemById;

[ApiController]
[Route("api/menuitem/get")]
public class GetMenuItemByIdController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuItemById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetMenuItemByIdQuery(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value);
    }
}