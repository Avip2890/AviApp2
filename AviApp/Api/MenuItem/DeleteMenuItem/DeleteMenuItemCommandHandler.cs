using AviApp.Interfaces;
using MediatR;

namespace AviApp.Api.MenuItem.DeleteMenuItem
{
    public class DeleteMenuItemCommandHandler(IMenuItemService menuItemService) : IRequestHandler<DeleteMenuItemCommand, bool>
    {
        public async Task<bool> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            
            return await menuItemService.DeleteMenuItemAsync(request.Id, cancellationToken);
        }
    }
}