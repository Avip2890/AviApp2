using AviApp.Models;
using MediatR;

namespace AviApp.Api.Order.OrderQueries;

public class GetOrderByIdQuery(int id) : IRequest<OrderDto?>
{
    public int Id { get; set; } = id;
}