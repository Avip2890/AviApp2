using AviApp.Api.MenuItem.CreateMenuItem;
using AviApp.Api.MenuItem.DeleteMenuItem;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AviApp.Api.MenuItem.MenuItemQueries;
using AviApp.Api.MenuItem.UpdateMenuItem;
using AviApp.Models;

namespace AviApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController(IMediator mediator) : ControllerBase
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

    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateMenuItemCommand(menuItemDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return CreatedAtAction(nameof(GetMenuItemById), new { id = result.Value.Id }, result.Value);
    }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteMenuItemCommand(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return NoContent();
    }
}
