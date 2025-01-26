using AviApp.Domain.Entities;
using AviApp.Results;

namespace AviApp.Interfaces;

public interface IMenuItemService
{
    Task<Result<List<MenuItem>>> GetAllMenuItemsAsync(CancellationToken cancellationToken);
    Task<Result<MenuItem>> GetMenuItemByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<MenuItem>> AddMenuItemAsync(MenuItem menuItem, CancellationToken cancellationToken);
    Task<Result<MenuItem>> UpdateMenuItemAsync(int id, MenuItem updatedMenuItem, CancellationToken cancellationToken);
    Task<Result<Deleted>> DeleteMenuItemAsync(int id, CancellationToken cancellationToken);
}