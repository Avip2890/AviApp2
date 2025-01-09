using AviApp.Domain.Context;
using AviApp.Interfaces;
using AviApp.Domain.Entities;
using AviApp.Results;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Services;

public class MenuItemService(AvipAppDbContext context) : IMenuItemService
{
  
    public async Task<Result<List<MenuItem>>> GetAllMenuItemsAsync(CancellationToken cancellationToken)
    {
        var menuItems = await context.MenuItems.AsNoTracking().ToListAsync(cancellationToken);

        if (!menuItems.Any())
        {
            return Result<List<MenuItem>>.Failure("No menu items found.");
        }

        return Result<List<MenuItem>>.Success(menuItems);
    }
    
    public async Task<Result<MenuItem>> GetMenuItemByIdAsync(int id, CancellationToken cancellationToken)
    {
        var menuItem = await context.MenuItems.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (menuItem == null)
        {
            return Result<MenuItem>.Failure($"Menu item with ID {id} not found.");
        }

        return Result<MenuItem>.Success(menuItem);
    }
    
    public async Task<Result<MenuItem>> AddMenuItemAsync(MenuItem menuItem, CancellationToken cancellationToken)
    {
        try
        {
            context.MenuItems.Add(menuItem);
            await context.SaveChangesAsync(cancellationToken);
            return Result<MenuItem>.Success(menuItem);
        }
        catch (Exception ex)
        {
            return Result<MenuItem>.Failure($"Failed to add menu item: {ex.Message}");
        }
    }
    
    public async Task<Result<MenuItem>> UpdateMenuItemAsync(int id, MenuItem updatedMenuItem, CancellationToken cancellationToken)
    {
        var menuItem = await context.MenuItems.FindAsync(new object[] { id }, cancellationToken);

        if (menuItem == null)
        {
            return Result<MenuItem>.Failure($"Menu item with ID {id} not found.");
        }

        menuItem.Name = updatedMenuItem.Name;
        menuItem.Description = updatedMenuItem.Description;
        menuItem.Price = updatedMenuItem.Price;
        menuItem.IsAvailable = updatedMenuItem.IsAvailable;

        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return Result<MenuItem>.Success(menuItem);
        }
        catch (Exception ex)
        {
            return Result<MenuItem>.Failure($"Failed to update menu item: {ex.Message}");
        }
    }
    
    public async Task<Result<bool>> DeleteMenuItemAsync(int id, CancellationToken cancellationToken)
    {
        var menuItem = await context.MenuItems.FindAsync(new object[] { id }, cancellationToken);

        if (menuItem == null)
        {
            return Result<bool>.Failure($"Menu item with ID {id} not found.");
        }

        try
        {
            context.MenuItems.Remove(menuItem);
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Failed to delete menu item: {ex.Message}");
        }
    }
}
