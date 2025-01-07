using AviApp.Api.Customer.CustomerQueries;
using AviApp.Interfaces;
using MediatR;
using AviApp.Models;

namespace AviApp.Api.Customer.CustomerHandlers;

public class GetCustomerByIdHandler(ICustomerService customerService)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerService.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer == null) return null;

       
        return new CustomerDto
        {
            Id = customer.Id,
            CustomerName = customer.CustomerName,
            Phone = customer.Phone
        };
    }
}