using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;

namespace AviApp.Queries.OrderQueries;

public class GetAllOrdersQuery : IRequest<IEnumerable<Order>>
{
}