using AviApp.Domain.Context;
using AviApp.Interfaces;
using AviApp.Domain.Entities;
using AviApp.Models;
using AviApp.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

public class MenuItemService : IMenuItemService
{
    private readonly AvipAppDbContext _context;

    public MenuItemService(AvipAppDbContext context)
    {
        _context = context;
    }

    // קבלת כל פריטי התפריט
    public async Task<IEnumerable<MenuItemDto>> GetAllMenuItemsAsync(CancellationToken cancellationToken)
    {
        var menuItems = await _context.MenuItems.ToListAsync(cancellationToken);
        return menuItems.Select(m => m.ToDto());
    }

    // קבלת פריט תפריט לפי מזהה
    public async Task<MenuItemDto?> GetMenuItemByIdAsync(int id, CancellationToken cancellationToken)
    {
        var menuItem = await _context.MenuItems.FindAsync(new object[] { id }, cancellationToken);
        return menuItem?.ToDto();
    }

    // יצירת פריט תפריט חדש
    public async Task<MenuItemDto> AddMenuItemAsync(MenuItemDto menuItemDto, CancellationToken cancellationToken)
    {
        var menuItem = new MenuItem
        {
            Name = menuItemDto.Name,
            Description = menuItemDto.Description,
            Price = menuItemDto.Price,
            IsAvailable = menuItemDto.IsAvailable,
            OrderId = null
        };

        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync(cancellationToken);
        return menuItem.ToDto();
    }

   
    public async Task<MenuItemDto?> UpdateMenuItemAsync(int id, MenuItemDto updatedMenuItemDto, CancellationToken cancellationToken)
    {
        var menuItem = await _context.MenuItems.FindAsync(new object[] { id }, cancellationToken);
        if (menuItem == null)
        {
            return null;
        }

        menuItem.Name = updatedMenuItemDto.Name;
        menuItem.Description = updatedMenuItemDto.Description;
        menuItem.Price = updatedMenuItemDto.Price;
        menuItem.IsAvailable = updatedMenuItemDto.IsAvailable;

        await _context.SaveChangesAsync(cancellationToken);
        return menuItem.ToDto();
    }

    // מחיקת פריט תפריט לפי מזהה
    public async Task<bool> DeleteMenuItemAsync(int id, CancellationToken cancellationToken)
    {
        var menuItem = await _context.MenuItems.FindAsync(new object[] { id }, cancellationToken);
        if (menuItem == null)
        {
            return false;
        }

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
