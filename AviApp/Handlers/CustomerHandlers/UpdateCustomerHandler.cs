using AviApp.Commands.CustomerCommands;
using AviApp.Interfaces;
using MediatR;

namespace AviApp.Handlers.CustomerHandlers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Models.Customer?>
{
    private readonly ICustomerService _customerService;

    public UpdateCustomerHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public Task<Models.Customer?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var updatedCustomer = new Models.Customer
        {
            CustomerName = request.CustomerName,
            Phone = request.Phone
        };

        return Task.FromResult(_customerService.UpdateCustomer(request.Id, updatedCustomer));
    }
}