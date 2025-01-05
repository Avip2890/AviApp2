using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Mappers;
using MediatR;

namespace AviApp.Api.Customer.UpdateCustomer;

public class UpdateCustomerCommandHandler(ICustomerService customerService) : IRequestHandler<UpdateCustomerCommand, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        // שליפת הלקוח מהמאגר
        var existingCustomer = await customerService.GetCustomerByIdAsync(request.Id, cancellationToken);
        
        if (existingCustomer == null)
        {
            return null; // הלקוח לא נמצא
        }

        // עדכון פרטי הלקוח
        existingCustomer.CustomerName = request.CustomerName;
        existingCustomer.Phone = request.Phone;

        // שמירת העדכונים
        var updatedCustomer = await customerService.UpdateCustomerAsync(existingCustomer, cancellationToken);

        // המרת הישות המעדכנת ל-DTO
        return updatedCustomer.ToDto();
    }
}