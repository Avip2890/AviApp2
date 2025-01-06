using AviApp.Models;
using AviApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems(CancellationToken cancellationToken)
    {
        var menuItems = await _menuItemService.GetAllMenuItemsAsync(cancellationToken);
        return Ok(menuItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuItemById(int id, CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemService.GetMenuItemByIdAsync(id, cancellationToken);
        if (menuItem == null)
        {
            return NotFound(new { Message = $"Menu item with Id {id} not found." });
        }

        return Ok(menuItem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var createdMenuItem = await _menuItemService.AddMenuItemAsync(menuItemDto, cancellationToken);
        return CreatedAtAction(nameof(GetMenuItemById), new { id = createdMenuItem.Id }, createdMenuItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDto updatedMenuItemDto, CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemService.UpdateMenuItemAsync(id, updatedMenuItemDto, cancellationToken);

        if (menuItem == null)
        {
            return NotFound(new { Message = $"Menu item with Id {id} not found." });
        }

        return Ok(menuItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id, CancellationToken cancellationToken)
    {
        var result = await _menuItemService.DeleteMenuItemAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound(new { Message = $"Menu item with Id {id} not found." });
        }

        return NoContent();
    }
}
