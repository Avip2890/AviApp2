using AviApp.Domain.Context;
using AviApp.Interfaces;
using AviApp.Domain.Entities;


namespace AviApp.Services;

public class MenuItemService : IMenuItemService
{
    private readonly AvipAppDbContext _context;

    public MenuItemService(AvipAppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<MenuItem> GetAllMenuItems() => _context.MenuItems.ToList();

    public MenuItem? GetMenuItemById(int id) => _context.MenuItems.Find(id);

    public MenuItem AddMenuItem(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);
        _context.SaveChanges();
        return menuItem;
    }

    public MenuItem UpdateMenuItem(int id, MenuItem updatedMenuItem)
    {
        var menuItem = _context.MenuItems.Find(id);
        if (menuItem != null)
        {
            menuItem.Name = updatedMenuItem.Name;
            menuItem.Description = updatedMenuItem.Description;
            menuItem.Price = updatedMenuItem.Price;
            menuItem.IsAvailable = updatedMenuItem.IsAvailable;
            _context.SaveChanges();
            return menuItem;
        }
        return null;
    }

    public bool DeleteMenuItem(int id)
    {
        var menuItem = _context.MenuItems.Find(id);
        if (menuItem != null)
        {
            _context.MenuItems.Remove(menuItem);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
