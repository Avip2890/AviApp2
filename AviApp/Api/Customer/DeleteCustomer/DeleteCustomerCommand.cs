using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customer.DeleteCustomer;

public record DeleteCustomerCommand(int Id) : IRequest<Result<bool>>;
