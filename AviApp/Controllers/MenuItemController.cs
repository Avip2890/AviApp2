using AviApp.Interfaces;
using AviApp.Models;
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
    public IActionResult GetAllMenuItems() => Ok(_menuItemService.GetAllMenuItems());

    [HttpGet("{id}")]
    public IActionResult GetMenuItemById(int id)
    {
        var menuItem = _menuItemService.GetMenuItemById(id);
        return menuItem != null ? Ok(menuItem) : NotFound();
    }

    [HttpPost]
    public IActionResult AddMenuItem([FromBody] MenuItem menuItem)
    {
        _menuItemService.AddMenuItem(menuItem);
        return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.Id }, menuItem);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMenuItem(int id, [FromBody] MenuItem updatedMenuItem)
    {
        _menuItemService.UpdateMenuItem(id, updatedMenuItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMenuItem(int id)
    {
        _menuItemService.DeleteMenuItem(id);
        return NoContent();
    }
}