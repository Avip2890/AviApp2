using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using MediatR;

namespace AviApp.Api.Customer.UpdateCustomer;

public class UpdateCustomerCommandHandler(ICustomerService customerService) : IRequestHandler<UpdateCustomerCommand, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomerResult = await customerService.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (!existingCustomerResult.IsSuccess || existingCustomerResult.Value == null)
        {
            return null; 
        }

        var existingCustomer = existingCustomerResult.Value;
        
        existingCustomer.CustomerName = request.CustomerName;
        existingCustomer.Phone = request.Phone;
        
        var updatedCustomerResult = await customerService.UpdateCustomerAsync(existingCustomer, cancellationToken);

        if (!updatedCustomerResult.IsSuccess || updatedCustomerResult.Value == null)
        {
            return null; 
        }
        
        return updatedCustomerResult.Value.ToDto();
    }
}