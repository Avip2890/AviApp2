using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;

namespace AviApp.Queries.MenuItemQueries;

public class GetMenuItemByIdQuery : IRequest<MenuItem?>
{
    public int Id { get; set; }

    public GetMenuItemByIdQuery(int id)
    {
        Id = id;
    }
}