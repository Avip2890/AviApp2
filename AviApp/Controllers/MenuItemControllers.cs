using AviApp.Api.MenuItem.CreateMenuItem;
using AviApp.Api.MenuItem.DeleteMenuItem;
using AviApp.Api.MenuItem.GetAllMenuItems;
using AviApp.Api.MenuItem.MenuItemQueries;
using AviApp.Api.MenuItem.UpdateMenuItem;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;

[Route("api/menuitems")]
public class MenuItemController(IMediator mediator) : AppBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllMenuItemsQuery(), cancellationToken);
        return ResultOf(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMenuItemById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetMenuItemByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateMenuItemCommand(
            menuItemDto.Name,
            menuItemDto.Description,
            menuItemDto.Price,
            menuItemDto.IsAvailable
        ), cancellationToken);

        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetMenuItemById), new { id = result.Value.Id }, result.Value) 
            : BadRequest(result.Errors);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateMenuItemCommand(
            id,
            menuItemDto.Name,
            menuItemDto.Price,
            menuItemDto.Description,
            menuItemDto.IsAvailable
        ), cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMenuItem(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteMenuItemCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }
}
