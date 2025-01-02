using AviApp.Models;
using MediatR;

namespace AviApp.Commands.OrderCommands;

public class AddOrderCommand : IRequest<Models.Order>
{
    public int CustomerId { get; set; }
    public List<MenuItem> Items { get; set; } = new();
    public DateTime OrderDate { get; set; }
}