using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;
using Order = AviApp.Models.OrderDto;

namespace AviApp.Queries.OrderQueries;

public class GetAllOrdersQuery : IRequest<IEnumerable<Order>>
{
}