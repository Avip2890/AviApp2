using AviApp.Commands.CustomerCommands;
using AviApp.Interfaces;
using MediatR;

namespace AviApp.Handlers.CustomerHandlers;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly ICustomerService _customerService;
    
    public DeleteCustomerHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    public Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_customerService.DeleteCustomer(request.Id));
    }
}