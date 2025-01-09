using AviApp.Models;
using AviApp.Interfaces;
using AviApp.Mappers;
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
        var result = await _menuItemService.GetAllMenuItemsAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        var menuItemsDto = result.Value.Select(m => m.ToDto()).ToList();
        return Ok(menuItemsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuItemById(int id, CancellationToken cancellationToken)
    {
        var result = await _menuItemService.GetMenuItemByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var menuItemEntity = menuItemDto.ToEntity();
        var result = await _menuItemService.AddMenuItemAsync(menuItemEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return CreatedAtAction(nameof(GetMenuItemById), new { id = result.Value.Id }, result.Value.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDto updatedMenuItemDto, CancellationToken cancellationToken)
    {
        var menuItemEntity = updatedMenuItemDto.ToEntity();
        menuItemEntity.Id = id;

        var result = await _menuItemService.UpdateMenuItemAsync(id, menuItemEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id, CancellationToken cancellationToken)
    {
        var result = await _menuItemService.DeleteMenuItemAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return NoContent();
    }
}
