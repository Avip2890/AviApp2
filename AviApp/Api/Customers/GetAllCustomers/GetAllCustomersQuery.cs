using AviApp.Models;
using MediatR;

namespace AviApp.Api.Customers.GetAllCustomers;

public  class GetAllCustomersQuery : IRequest<List<CustomerDto>>
{
    
}