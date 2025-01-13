using AviApp.Interfaces;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customer.DeleteCustomer;

public class DeleteCustomerCommandHandler(ICustomerService customerService)
    : IRequestHandler<DeleteCustomerCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        // קריאה למחיקת הלקוח בשירות
        var result = await customerService.DeleteCustomerAsync(request.Id, cancellationToken);

        if (!result.IsSuccess)
        {
            return Result<bool>.Failure(result.Error);
        }

        return Result<bool>.Success(true);
    }
}