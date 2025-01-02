using AviApp.Models;
using MediatR;

namespace AviApp.Queries.MenuItemQueries;

public class GetAllMenuItemsQuery : IRequest<IEnumerable<MenuItem>>
{
}