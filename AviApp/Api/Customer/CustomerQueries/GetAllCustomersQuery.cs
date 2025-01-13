
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Customer.CustomerQueries;

public  class GetAllCustomersQuery : IRequest<List<CustomerDto>>
{
    
}