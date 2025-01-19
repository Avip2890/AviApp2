
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
            return Result<CustomerDto>.Failure($"Customers with ID {request.Id} not found.");
        }

        // שימוש ב-Mapper להמרה ל-DTO
        return Result<CustomerDto>.Success(result.Value.ToDto());
    }
}