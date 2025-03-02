using AviApp.Domain.Entities;
using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using AviApp.Mappers;
using MediatR;

namespace AviApp.Api.Customers;

public class CreateCustomerCommandHandler(ICustomerService customerService) 
    : IRequestHandler<CreateCustomerCommand, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerCreateRequest = request.CreateCustomerRequest;
        var customerEntity = new Customer
        {
            
            CustomerName = customerCreateRequest.CustomerName,
            Phone = customerCreateRequest.Phone
        };

        var result = await customerService.CreateCustomerAsync(customerEntity, cancellationToken);

        return result.IsSuccess ? result.Value.ToDto() : result.Errors;
    }
}
