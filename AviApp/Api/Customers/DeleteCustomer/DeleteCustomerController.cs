using Microsoft.AspNetCore.Mvc;
using MediatR;


namespace AviApp.Api.Customers.DeleteCustomer;

[ApiController]
[Route("api/customer/delete/{id}")]
public class DeleteCustomerController(IMediator mediator) : ControllerBase
{
    [HttpDelete]
    public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new DeleteCustomerCommand(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return NoContent();
    }
}