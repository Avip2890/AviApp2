using MediatR;

namespace AviApp.Api.Customer.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteCustomerCommand(int id)
    {
        Id = id;
    }
}