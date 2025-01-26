using AviApp.Api.MenuItem.CreateMenuItem;
using AviApp.Api.MenuItem.DeleteMenuItem;
using AviApp.Api.MenuItem.GetAllMenuItems;
using AviApp.Api.MenuItem.MenuItemQueries;
using AviApp.Api.MenuItem.UpdateMenuItem;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;


[Route("api/menuitem")]
public class MenuItemController(IMediator mediator) : AppBaseController
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllMenuItems(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllMenuItemsQuery(), cancellationToken);
        return ResultOf(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetMenuItemById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetMenuItemByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDto menuItemDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateMenuItemCommand(menuItemDto), cancellationToken);
        return ResultOf(result,
            successResult: CreatedAtAction(nameof(GetMenuItemById), new { id = result.Value.Id }, result.Value));
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateMenuItemCommand(id, menuItemDto), cancellationToken);
        return ResultOf(result);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteMenuItem(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteMenuItemCommand(id), cancellationToken);
        return ResultOf(result, successResult: NoContent());
    }
}
