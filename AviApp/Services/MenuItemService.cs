using AviApp.Interfaces;
using AviApp.Models;


namespace AviApp.Services;

public class MenuItemService : IMenuItemService
{
    private readonly List<MenuItem> _menuItems = new List<MenuItem>();

    public IEnumerable<MenuItem> GetAllMenuItems() => _menuItems;

    public MenuItem? GetMenuItemById(int id) => _menuItems.FirstOrDefault(m => m.Id == id);

    public void AddMenuItem(MenuItem menuItem)
    {
        menuItem.Id = _menuItems.Count + 1; 
        _menuItems.Add(menuItem);
    }

    public void UpdateMenuItem(int id, MenuItem updatedMenuItem)
    {
        var menuItem = GetMenuItemById(id);
        if (menuItem != null)
        {
            menuItem.Name = updatedMenuItem.Name;
            menuItem.Description = updatedMenuItem.Description;
            menuItem.Price = updatedMenuItem.Price;
            menuItem.IsAvailable = updatedMenuItem.IsAvailable;
        }
    }

    public void DeleteMenuItem(int id)
    {
        var menuItem = GetMenuItemById(id);
        if (menuItem != null)
        {
            _menuItems.Remove(menuItem);
        }
    }
}