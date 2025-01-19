using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace AviApp.Api.Order.UpdateOrder;

[ApiController]
[Route("api/order/update")]
public class UpdateOrderController(IMediator mediator) : ControllerBase
{
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateOrderCommand(id, orderDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value);
    }
}