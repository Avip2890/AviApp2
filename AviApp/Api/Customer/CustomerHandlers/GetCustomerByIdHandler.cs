using AviApp.Api.Customer.CustomerQueries;
using AviApp.Interfaces;
using MediatR;
using AviApp.Models;
using AviApp.Results;

namespace AviApp.Api.Customer.CustomerHandlers;

public class GetCustomerByIdHandler(ICustomerService customerService)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await customerService.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (!result.IsSuccess) return null;

        var customer = result.Value;

        return new CustomerDto
        {
            Id = customer.Id,
            CustomerName = customer.CustomerName,
            Phone = customer.Phone
        };
    }
}