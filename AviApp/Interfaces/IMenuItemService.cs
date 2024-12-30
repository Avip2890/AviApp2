
using AviApp.Models;

namespace AviApp.Interfaces;

public interface IMenuItemService
{
    IEnumerable<MenuItem> GetAllMenuItems();
    MenuItem? GetMenuItemById(int id);
    void AddMenuItem(MenuItem menuItem);
    void UpdateMenuItem(int id, MenuItem updatedMenuItem);
    void DeleteMenuItem(int id);
}