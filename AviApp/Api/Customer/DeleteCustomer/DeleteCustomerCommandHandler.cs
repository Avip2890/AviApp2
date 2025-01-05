using AviApp.Interfaces;
using MediatR;

namespace AviApp.Api.Customer.DeleteCustomer;

public class DeleteCustomerCommandHandler(ICustomerService customerService) : IRequestHandler<DeleteCustomerCommand, bool>
{
    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        // קריאה ל-DeleteCustomer בשירות
        return await customerService.DeleteCustomerAsync(request.Id, cancellationToken);
    }
}