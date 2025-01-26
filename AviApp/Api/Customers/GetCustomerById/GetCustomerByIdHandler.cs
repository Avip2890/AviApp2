using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using AviApp.Results;
using MediatR;


namespace AviApp.Api.Customers.GetCustomerById;

public class GetCustomerByIdHandler(ICustomerService customerService)
    : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await customerService.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (!result.IsSuccess || result.Value == null)
        {
            return Error.NotFound("Customer ID: {id} not found");
        }
        
        return Result<CustomerDto>.Success(result.Value.ToDto());
    }
}