using AviApp.Api.Customer.CustomerQueries;
using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Customer.CustomerHandlers
{
    public class GetAllCustomersHandler(ICustomerService customerService)
        : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            
            var customers = await customerService.GetAllCustomersAsync(cancellationToken);
            
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                CustomerName = c.CustomerName,
                Phone = c.Phone
            }).ToList();
        }
    }
}