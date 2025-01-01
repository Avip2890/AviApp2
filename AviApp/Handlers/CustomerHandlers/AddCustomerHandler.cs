using AviApp.Commands.CustomerCommands;
using AviApp.Interfaces;
using MediatR;

namespace AviApp.Handlers.CustomerHandlers;

public class AddCustomerHandler : IRequestHandler<AddCustomerCommand, Models.Customer>
{
    private readonly ICustomerService _customerService;

    public AddCustomerHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public Task<Models.Customer> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var newCustomer = new Models.Customer
        {
            CustomerName = request.CustomerName,
            Phone = request.Phone
        };

        return Task.FromResult(_customerService.CreateCustomer(newCustomer));
    }
}