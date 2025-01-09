using AviApp.Api.Customer.CustomerQueries;
using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Customer.CustomerHandlers;

public class GetAllCustomersHandler(ICustomerService customerService)
    : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
{
    public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var result = await customerService.GetAllCustomersAsync(cancellationToken);
        
        if (!result.IsSuccess)
        {
            throw new Exception(result.Error); 
        }
        
        return result.Value.Select(c => new CustomerDto
        {
            Id = c.Id,
            CustomerName = c.CustomerName,
            Phone = c.Phone
        }).ToList();
    }
}