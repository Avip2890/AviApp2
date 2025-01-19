using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customers.DeleteCustomer;

public record DeleteCustomerCommand(int Id) : IRequest<Result<bool>>;
