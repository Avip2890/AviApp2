using AviApp.Models;
using MediatR;

namespace AviApp.Commands.OrderCommands;

public class UpdateOrderCommand : IRequest<Models.Order?>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<MenuItem> Items { get; set; } = new();
    public DateTime OrderDate { get; set; }
}