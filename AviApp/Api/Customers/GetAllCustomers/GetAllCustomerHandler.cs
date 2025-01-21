using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Results;
using MediatR;
using AviApp.Api.Customers.GetAllCustomers;
using AviApp.Mappers;

namespace AviApp.Api.Customers.GetAllCustomers;

public class GetAllCustomersHandler(ICustomerService customerService)
    : IRequestHandler<GetAllCustomersQuery, Result<List<CustomerDto>>>
{
    public async Task<Result<List<CustomerDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var result = await customerService.GetAllCustomersAsync(cancellationToken);
        if (!result.IsSuccess)
        {
            return Result<List<CustomerDto>>.Failure(result.Error);
        }

        return Result<List<CustomerDto>>.Success(result.Value.Select(c => c.ToDto()).ToList());
    }
}