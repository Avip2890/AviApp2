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
        return result.IsSuccess ? new Deleted() : result.Errors;

    }
}