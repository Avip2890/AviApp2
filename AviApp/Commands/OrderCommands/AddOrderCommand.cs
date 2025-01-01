using MediatR;

namespace AviApp.Commands.OrderCommands;

public class AddOrderCommand : IRequest<Models.Order>
{
    public int CustomerId { get; set; }
    public List<int> MenuItemIds { get; set; } = new List<int>();
    public DateTime OrderDate { get; set; }
}