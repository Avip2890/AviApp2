/*using AviApp.Interfaces;
using AviApp.Models;
using AviApp.Queries.CustomerQueries;
using MediatR;
using AviApp.Domain.Entities;

namespace AviApp.Handlers.CustomerHandlers;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
{
    private readonly ICustomerService _customerService;

    public GetCustomerByIdHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
       
        return Task.FromResult(_customerService.GetCustomerById(request.Id));
    }
}*/