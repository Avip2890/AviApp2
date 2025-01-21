using AviApp.Models;
using AviApp.Results;
using MediatR;

namespace AviApp.Api.Customers.GetAllCustomers;

public record GetAllCustomersQuery() : IRequest<Result<List<CustomerDto>>>;