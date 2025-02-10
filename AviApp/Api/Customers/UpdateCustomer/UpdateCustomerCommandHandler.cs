using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customers.UpdateCustomer;

public class UpdateCustomerCommandHandler(ICustomerService customerService)
    : IRequestHandler<UpdateCustomerCommand, Result<CustomerDto>>
{ 
    public async Task<Result<CustomerDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomerResult = await customerService.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (!existingCustomerResult.IsSuccess)
        {
            return Error.NotFound($"Customer with ID {request.Id} not found.");
        }

        var existingCustomer = existingCustomerResult.Value;
        
        existingCustomer.CustomerName = request.CustomerName;
        existingCustomer.Phone = request.Phone;

        var updatedCustomerResult = await customerService.UpdateCustomerAsync(existingCustomer, cancellationToken);

       
        return (updatedCustomerResult.IsSuccess)
            ? updatedCustomerResult.Value.ToDto()
            : Error.BadRequest("Update Failed");
    }
}