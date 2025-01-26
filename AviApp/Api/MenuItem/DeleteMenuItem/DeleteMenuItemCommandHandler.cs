using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.MenuItem.DeleteMenuItem;

public class DeleteMenuItemHandler(IMenuItemService menuItemService)
    : IRequestHandler<DeleteMenuItemCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
    {
        var result = await menuItemService.DeleteMenuItemAsync(request.Id, cancellationToken);
        if (!result.IsSuccess)
        {
            return Error.BadRequest("Delete Failed"); 
        }

        return new Deleted();
    }
}