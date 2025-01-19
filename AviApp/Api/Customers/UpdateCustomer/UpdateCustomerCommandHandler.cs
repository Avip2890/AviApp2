using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customer.UpdateCustomer;

public class UpdateCustomerCommandHandler(ICustomerService customerService)
    : IRequestHandler<UpdateCustomerCommand, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        // שליפת הלקוח הקיים
        var existingCustomerResult = await customerService.GetCustomerByIdAsync(request.CustomerDto.Id, cancellationToken);

        if (!existingCustomerResult.IsSuccess || existingCustomerResult.Value == null)
        {
            return Result<CustomerDto>.Failure($"Customers with ID {request.CustomerDto.Id} not found.");
        }

        var existingCustomer = existingCustomerResult.Value;

        // עדכון פרטי הלקוח
        existingCustomer.CustomerName = request.CustomerDto.CustomerName;
        existingCustomer.Phone = request.CustomerDto.Phone;

        // שמירת העדכון
        var updatedCustomerResult = await customerService.UpdateCustomerAsync(existingCustomer, cancellationToken);

        if (!updatedCustomerResult.IsSuccess || updatedCustomerResult.Value == null)
        {
            return Result<CustomerDto>.Failure(updatedCustomerResult.Error);
        }

        // החזרת הלקוח המעודכן כ-DTO
        return Result<CustomerDto>.Success(updatedCustomerResult.Value.ToDto());
    }
}