using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Queries.CustomerQueries;
using MediatR;

namespace AviApp.Handlers.CustomerHandlers;

public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, List<Customer>>
{
    private readonly ICustomerService _customerService;

    public GetAllCustomersHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public Task<List<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_customerService.GetAllCustomers());
    }
}