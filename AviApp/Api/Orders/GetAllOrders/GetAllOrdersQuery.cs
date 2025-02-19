using AviApp.Models;
using MediatR;
using AviApp.Results;

namespace AviApp.Api.Orders.GetAllOrders;
public record GetAllOrdersQuery() : IRequest<Result<List<OrderDto>>>;
