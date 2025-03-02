using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customers;

public record CreateCustomerCommand(CustomerDto CreateCustomerRequest) : IRequest<Result<CustomerDto>>;

