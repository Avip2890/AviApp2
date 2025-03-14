using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using MediatR;
using AviApp.Mappers;

namespace AviApp.Api.Customers.GetAllCustomers;

public class GetAllCustomersHandler(ICustomerService customerService)
    : IRequestHandler<GetAllCustomersQuery, Result<List<CustomerDto>>>
{
    public async Task<Result<List<CustomerDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var result = await customerService.GetAllCustomersAsync(cancellationToken);
        return result.IsSuccess 
            ? result.Value.Select(c => c.ToDto()).ToList()
            : Error.BadRequest("Customer service returned an empty result");
    }
}