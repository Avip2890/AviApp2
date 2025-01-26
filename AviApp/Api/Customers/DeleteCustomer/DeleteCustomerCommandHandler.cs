using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customers.DeleteCustomer;

public class DeleteCustomerCommandHandler(ICustomerService customerService)
    : IRequestHandler<DeleteCustomerCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
       
        var result = await customerService.DeleteCustomerAsync(request.Id, cancellationToken);

        if (!result.IsSuccess)
        {
            return Error.BadRequest("Customer ");
        }

        return new Deleted();
    }
}