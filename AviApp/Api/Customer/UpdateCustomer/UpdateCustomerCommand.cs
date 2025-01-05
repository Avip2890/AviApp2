using AviApp.Models;
using MediatR;

namespace AviApp.Api.Customer.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<CustomerDto?>
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}