using System.ComponentModel.DataAnnotations;
using AviApp.Api.MenuItem.CreateMenuItem;
using AviApp.Api.MenuItem.DeleteMenuItem;
using AviApp.Api.MenuItem.GetAllMenuItems;
using AviApp.Api.MenuItem.GetMenuItemById;
using AviApp.Api.MenuItem.UpdateMenuItem;
using AviApp.Attributes;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AviApp.Controllers;


public class MenuItemController(IMediator mediator) : AppBaseController
{
    /// <summary>
    /// Get all menu items
    /// </summary>
    /// <remarks>Get all menu items, no authentication required</remarks>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("/api/menuitems")]
    [ValidateModelState]
    [SwaggerOperation("GetMenuItems")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<MenuItemDto>), description: "OK")]
    public virtual async Task<IActionResult> GetMenuItems(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllMenuItemsQuery(), cancellationToken);
        return ResultOf(result);
    }
    
    /// <summary>
    /// Get a menu item by id
    /// </summary>
    /// <remarks>Get a menu item by id, no authentication required</remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("/api/menuitems/{id}")]
    [ValidateModelState]
    [SwaggerOperation("GetMenuItem")]
    public virtual async Task<IActionResult> GetMenuItem([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetMenuItemByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }

    /// <summary>
    /// Add a new menu item
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="menuItemDto"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="201">New menu item is created</response>
    [HttpPost]
    [Route("/api/menuitems")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("AddMenuItem")]
    [SwaggerResponse(statusCode: 201, type: typeof(List<MenuItemDto>), description: "New menu item is created")]
    [Authorize (Roles = "Admin")]
    public virtual async Task<IActionResult> AddMenuItem([FromBody]MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateMenuItemCommand(menuItemDto), cancellationToken);

        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetMenuItem), new { id = result.Value.Id }, result.Value) 
            : BadRequest(result.Errors);
    }

    /// <summary>
    /// Update a menu item
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="id"></param>
    /// <param name="menuItemDto"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">Menu item is updated</response>
    [HttpPut]
    [Route("/api/menuitems/{id}")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("UpdateMenuItem")]
    [Authorize (Roles = "Admin")]
    public virtual async Task<IActionResult> UpdateMenuItem([FromRoute (Name = "id")][Required]int id, [FromBody]MenuItemDto? menuItemDto, CancellationToken cancellationToken)
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

    /// <summary>
    /// Delete a menu item
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">Menu item is deleted</response>
    [HttpDelete]
    [Route("/api/menuitems/{id}")]
    [ValidateModelState]
    [SwaggerOperation("DeleteMenuItem")]
    [Authorize (Roles = "Admin")]
    public virtual async Task<IActionResult> DeleteMenuItem([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteMenuItemCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }
}
