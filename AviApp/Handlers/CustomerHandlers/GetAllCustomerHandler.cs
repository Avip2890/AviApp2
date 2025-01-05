using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Queries.CustomerQueries;
using MediatR;
using AviApp.Domain.Entities;

namespace AviApp.Handlers.CustomerHandlers;

public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, List<Customer>>
{
    private readonly ICustomerService _customerService;

    public GetAllCustomersHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<List<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _customerService.GetAllCustomersAsync(cancellationToken);
    }
}