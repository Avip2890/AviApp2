using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;
using MenuItem = AviApp.Models.MenuItemDto;

namespace AviApp.Queries.MenuItemQueries;

public class GetAllMenuItemsQuery : IRequest<IEnumerable<MenuItem>>
{
}