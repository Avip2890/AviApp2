using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;

namespace AviApp.Queries.CustomerQueries;

public class GetAllCustomersQuery : IRequest<List<CustomerDto>>
{
    
}