using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;

namespace AviApp.Commands.OrderCommands;

public class AddOrderCommand : IRequest<Order>
{
    public int CustomerId { get; set; }
    public List<MenuItem> Items { get; set; } = new();
    public DateTime OrderDate { get; set; }
}