using MediatR;

namespace AviApp.Api.Order.DeleteOrder;

public abstract class DeleteOrderCommand(int id) : IRequest<bool>

{
public int Id { get; set; } = id;
}