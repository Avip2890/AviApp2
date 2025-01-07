using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Customer.CustomerQueries;

public abstract class GetAllCustomersQuery : IRequest<List<CustomerDto>>
{
    
}