using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Queries.CustomerQueries;
using MediatR;

namespace AviApp.Handlers.CustomerHandlers
{
    public class GetAllCustomersHandler(ICustomerService customerService)
        : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            // Fetch customers from the service
            var customers = await customerService.GetAllCustomersAsync(cancellationToken);

            // Map the entities to DTOs
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                CustomerName = c.CustomerName,
                Phone = c.Phone
            }).ToList();
        }
    }
}