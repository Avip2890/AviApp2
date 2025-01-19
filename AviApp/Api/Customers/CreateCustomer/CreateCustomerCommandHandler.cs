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
     
        var customerEntity = request.CustomerDto.ToEntity();

      
        var result = await customerService.CreateCustomerAsync(customerEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return Result<CustomerDto>.Failure(result.Error);
        }

        return Result<CustomerDto>.Success(result.Value.ToDto());
    }
}