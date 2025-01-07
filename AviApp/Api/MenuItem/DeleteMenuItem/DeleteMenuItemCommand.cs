namespace AviApp.Api.MenuItem.DeleteMenuItem;

using MediatR;



public abstract class DeleteMenuItemCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteMenuItemCommand(int id)
    {
        Id = id;
    }
}