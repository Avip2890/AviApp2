using AviApp.Domain.Entities;
using MediatR;

namespace AviApp.Queries.CustomerQueries;

public class GetAllCustomersQuery : IRequest<List<Customer>>
{
    
}