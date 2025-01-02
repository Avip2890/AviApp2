using AviApp.Models;
using MediatR;

namespace AviApp.Queries.OrderQueries;

public class GetOrderByIdQuery : IRequest<Order?>
{
    public int Id { get; set; }

    public GetOrderByIdQuery(int id)
    {
        Id = id;
    }
}