using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Customer;


public class CreateCustomerCommandHandler(ICustomerService customerService): IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerDb = request.CustomerDto.ToEntity();

        customerDb = await customerService.CreateCustomer(customerDb, cancellationToken);

        return customerDb.ToDto();
    }
}