using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.MenuItem.CreateMenuItem;

[ApiController]
[Route("api/menuitem/create")]
public class CreateMenuItemController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateMenuItemCommand(menuItemDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return CreatedAtAction(nameof(CreateMenuItem), new { id = result.Value.Id }, result.Value);
    }
}