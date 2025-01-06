using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;
using Order = AviApp.Models.OrderDto;

namespace AviApp.Queries.OrderQueries;

public class GetOrderByIdQuery : IRequest<Order?>
{
    public int Id { get; set; }

    public GetOrderByIdQuery(int id)
    {
        Id = id;
    }
}