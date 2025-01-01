using MediatR;
using AviApp.Models;

namespace AviApp.Commands.CustomerCommands;

public class AddCustomerCommand : IRequest<Models.Customer>
{
    public string CustomerName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}