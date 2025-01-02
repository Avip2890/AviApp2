using AviApp.Models;
using MediatR;

namespace AviApp.Commands.CustomerCommands;

public class UpdateCustomerCommand : IRequest<Customer?>
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}