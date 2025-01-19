using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customer.UpdateCustomer;

public record UpdateCustomerCommand(CustomerDto CustomerDto) : IRequest<Result<CustomerDto>>;
