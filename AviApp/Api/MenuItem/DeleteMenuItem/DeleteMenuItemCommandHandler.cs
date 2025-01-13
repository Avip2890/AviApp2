using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.MenuItem.DeleteMenuItem;

public class DeleteMenuItemHandler(IMenuItemService menuItemService)
    : IRequestHandler<DeleteMenuItemCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
    {
        var result = await menuItemService.DeleteMenuItemAsync(request.Id, cancellationToken);
        if (!result.IsSuccess)
        {
            return Result<bool>.Failure(result.Error);
        }

        return Result<bool>.Success(true);
    }
}