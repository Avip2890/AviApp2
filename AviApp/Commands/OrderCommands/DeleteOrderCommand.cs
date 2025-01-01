using MediatR;

namespace AviApp.Commands.OrderCommands;

public class DeleteOrderCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteOrderCommand(int id)
    {
        Id = id;
    }
}