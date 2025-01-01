using MediatR;

namespace AviApp.Commands.OrderCommands;

public class UpdateOrderCommand : IRequest<Models.Order?>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<int> MenuItemIds { get; set; } = new List<int>();
    public DateTime OrderDate { get; set; }
}