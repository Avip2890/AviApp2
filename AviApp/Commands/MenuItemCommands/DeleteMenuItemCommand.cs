using MediatR;

namespace AviApp.Commands.MenuItemCommands;

public class DeleteMenuItemCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteMenuItemCommand(int id)
    {
        Id = id;
    }
}