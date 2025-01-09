namespace AviApp.Api.MenuItem.DeleteMenuItem;

using MediatR;



public  class DeleteMenuItemCommand(int id) : IRequest<bool>
{
    public int Id { get; set; } = id;
}