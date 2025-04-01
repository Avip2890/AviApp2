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
        
        return menuItems;
    }
    
    public async Task<Result<MenuItem>> GetMenuItemByIdAsync(int id, CancellationToken cancellationToken)
    {
        var menuItem = await context.MenuItems.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (menuItem == null)
        {
            return Error.NotFound("MenuItem Not Found");
        }

        return menuItem;
    }
    
    public async Task<Result<MenuItem>> AddMenuItemAsync(MenuItem menuItem, CancellationToken cancellationToken)
    {
        try
        {
            context.MenuItems.Add(menuItem);
            await context.SaveChangesAsync(cancellationToken);
            return menuItem;
        }
        catch  
        {
            return Error.BadRequest("Couldn't Add new MenuItem");
        }
    }
    
    public async Task<Result<MenuItem>> UpdateMenuItemAsync(int id, MenuItem updatedMenuItem, CancellationToken cancellationToken)
    {
        var menuItem = await context.MenuItems.FindAsync(new object[] { id }, cancellationToken);

        if (menuItem == null)
        {
            return Error.NotFound("MenuItem not found");
        }

        menuItem.Name = updatedMenuItem.Name;
        menuItem.Description = updatedMenuItem.Description;
        menuItem.Price = updatedMenuItem.Price;
        menuItem.IsAvailable = updatedMenuItem.IsAvailable;
        menuItem.ImageUrl = updatedMenuItem.ImageUrl;
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return menuItem;
        }
        catch
        {
            return Error.BadRequest("Couldn't Update MenuItem");
        }
    }
    
    public async Task<Result<Deleted>> DeleteMenuItemAsync(int id, CancellationToken cancellationToken)
    {
        var menuItem = await context.MenuItems.FindAsync(new object[] { id }, cancellationToken);

        if (menuItem == null)
        {
            return Error.NotFound("MenuItem Not Found");
        }

        try
        {
            context.MenuItems.Remove(menuItem);
            await context.SaveChangesAsync(cancellationToken);
            return new Deleted();
        }
        catch 
        {
            return Error.BadRequest("Couldn't Delete MenuItem");
        }
    }
}