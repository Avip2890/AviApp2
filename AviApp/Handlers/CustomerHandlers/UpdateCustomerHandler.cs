/*using AviApp.Commands.CustomerCommands;
using AviApp.Interfaces;
using MediatR;
using AviApp.Domain.Entities;
namespace AviApp.Handlers.CustomerHandlers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Models.CustomerDto?>
{
    private readonly ICustomerService _customerService;

    public UpdateCustomerHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public Task<Models.CustomerDto?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var updatedCustomer = new Models.CustomerDto
        {
            CustomerName = request.CustomerName,
            Phone = request.Phone
        };

        return Task.FromResult(_customerService.UpdateCustomer(request.Id, updatedCustomer));
    }
}*/