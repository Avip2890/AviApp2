using AviApp.Interfaces;
using MediatR;

namespace AviApp.Api.MenuItem.DeleteMenuItem
{
    public class DeleteMenuItemCommandHandler(IMenuItemService menuItemService)
        : IRequestHandler<DeleteMenuItemCommand, bool>
    {
        public async Task<bool> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            var result = await menuItemService.DeleteMenuItemAsync(request.Id, cancellationToken);

            if (!result.IsSuccess)
            {
                throw new InvalidOperationException(result.Error);
            }

            return result.Value;
        }
    }
}