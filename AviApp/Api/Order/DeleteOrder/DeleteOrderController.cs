using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.Order.DeleteOrder;

[ApiController]
[Route("api/order/delete")]
public class DeleteOrderController(IMediator mediator) : ControllerBase
{
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteOrderCommand(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return NoContent();
    }
}