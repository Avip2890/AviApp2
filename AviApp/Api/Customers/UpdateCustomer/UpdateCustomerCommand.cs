using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customers.UpdateCustomer;

public record UpdateCustomerCommand(int Id, string CustomerName, string Phone) : IRequest<Result<CustomerDto>>;