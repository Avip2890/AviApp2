using AviApp.Api.Customer.UpdateCustomer;
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
        var existingCustomerResult = await customerService.GetCustomerByIdAsync(request.CustomerDto.Id, cancellationToken);

        if (!existingCustomerResult.IsSuccess || existingCustomerResult.Value == null)
        {
            return Error.NotFound($"Customers with ID {request.CustomerDto.Id} not found.");
        }

        var existingCustomer = existingCustomerResult.Value;
        
        existingCustomer.CustomerName = request.CustomerDto.CustomerName;
        existingCustomer.Phone = request.CustomerDto.Phone;

   
        var updatedCustomerResult = await customerService.UpdateCustomerAsync(existingCustomer, cancellationToken);

        if (!updatedCustomerResult.IsSuccess || updatedCustomerResult.Value == null)
        {
            return Error.BadRequest("Update Failed ");
        }
        
        return Result<CustomerDto>.Success(updatedCustomerResult.Value.ToDto());
    }
}