using AviApp.Models;
using MediatR;

namespace AviApp.Api.Customer;

public record CreateCustomerCommand(CustomerDto CustomerDto) : IRequest<CustomerDto>;

