using AviApp.Domain.Entities;
using MenuItem = AviApp.Models.MenuItemDto;

namespace AviApp.Interfaces;

public interface IMenuItemService
{
    Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync(CancellationToken cancellationToken);
    Task<MenuItem?> GetMenuItemByIdAsync(int id, CancellationToken cancellationToken);
    Task<MenuItem> AddMenuItemAsync(MenuItem menuItem, CancellationToken cancellationToken);
    Task<MenuItem?> UpdateMenuItemAsync(int id, MenuItem updatedMenuItem, CancellationToken cancellationToken);
    Task<bool> DeleteMenuItemAsync(int id, CancellationToken cancellationToken);
}