using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customer;

public record CreateCustomerCommand(CustomerDto CustomerDto) : IRequest<Result<CustomerDto>>;

