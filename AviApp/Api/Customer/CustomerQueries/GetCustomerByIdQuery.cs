using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customer.CustomerQueries;

public record GetCustomerByIdQuery(int Id) : IRequest<Result<CustomerDto>>;