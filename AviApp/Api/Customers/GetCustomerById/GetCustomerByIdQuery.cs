using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customers.GetCustomerById;

public record GetCustomerByIdQuery(int Id) : IRequest<Result<CustomerDto>>;