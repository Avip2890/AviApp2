using MediatR;
using AviApp.Models;

namespace AviApp.Api.Customer.CustomerQueries;

public class GetCustomerByIdQuery(int id) : IRequest<CustomerDto?>
{
    public int Id { get; } = id;
}