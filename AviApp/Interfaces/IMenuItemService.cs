
using AviApp.Domain.Entities;

namespace AviApp.Interfaces;

public interface IMenuItemService
{
    IEnumerable<MenuItem> GetAllMenuItems();
    MenuItem? GetMenuItemById(int id);
    MenuItem AddMenuItem(MenuItem menuItem); 
    MenuItem? UpdateMenuItem(int id, MenuItem updatedMenuItem);
    bool DeleteMenuItem(int id);
}