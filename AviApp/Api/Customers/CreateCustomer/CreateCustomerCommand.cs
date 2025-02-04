using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customers;

public record CreateCustomerCommand(string CustomerName, string Phone) : IRequest<Result<CustomerDto>>;

