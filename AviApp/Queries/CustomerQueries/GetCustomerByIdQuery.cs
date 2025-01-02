using AviApp.Models;
using MediatR;

namespace AviApp.Queries.CustomerQueries;

public class GetCustomerByIdQuery : IRequest<Customer?>
{
    public int Id { get; set; }

    public GetCustomerByIdQuery(int id)
    {
        Id = id;
    }
}