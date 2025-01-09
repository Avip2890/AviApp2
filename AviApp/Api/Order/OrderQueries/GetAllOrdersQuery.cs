using AviApp.Models;
using MediatR;
using System.Collections.Generic;

namespace AviApp.Api.Order.OrderQueries
{
    public class GetAllOrdersQuery : IRequest<List<OrderDto>>
    {
 
    }
}