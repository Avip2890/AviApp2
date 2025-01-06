using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;
using MenuItem = AviApp.Models.MenuItemDto;

namespace AviApp.Queries.MenuItemQueries;

public class GetMenuItemByIdQuery : IRequest<MenuItem?>
{
    public int Id { get; set; }

    public GetMenuItemByIdQuery(int id)
    {
        Id = id;
    }
}