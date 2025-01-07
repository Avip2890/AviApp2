using AviApp.Models;
using MediatR;

namespace AviApp.Api.Order.OrderQueries;

public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
}