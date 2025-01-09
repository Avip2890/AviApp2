using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Customer;

public class CreateCustomerCommandHandler(ICustomerService customerService)
    : IRequestHandler<CreateCustomerCommand, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await customerService.CreateCustomerAsync(request.CustomerDto.ToEntity(), cancellationToken);
        
        if (!result.IsSuccess)
        {
         
            throw new Exception(result.Error); 
        }
        
        return result.Value.ToDto();
    }
}