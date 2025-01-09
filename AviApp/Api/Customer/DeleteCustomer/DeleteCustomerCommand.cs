using MediatR;

namespace AviApp.Api.Customer.DeleteCustomer;

public  class DeleteCustomerCommand(int id) : IRequest<bool>
{
    public int Id { get; set; } = id;
}