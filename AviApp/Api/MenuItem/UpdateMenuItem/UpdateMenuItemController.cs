using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.MenuItem.UpdateMenuItem;

[ApiController]
[Route("api/menuitem/update")]
public class UpdateMenuItemController(IMediator mediator) : ControllerBase
{
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateMenuItemCommand(id, menuItemDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value);
    }
}