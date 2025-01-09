using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customer.DeleteCustomer;

public class DeleteCustomerCommandHandler(ICustomerService customerService)
    : IRequestHandler<DeleteCustomerCommand, bool>
{
    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await customerService.DeleteCustomerAsync(request.Id, cancellationToken);
        
        if (!result.IsSuccess)
        {
            throw new Exception(result.Error); 
        }
        
        return result.Value;
    }
}