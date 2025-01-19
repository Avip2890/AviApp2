using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.Order.CreateOrder;

[ApiController]
[Route("api/order/create")]
public class CreateOrderController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateOrderCommand(orderDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return CreatedAtAction(nameof(CreateOrder), new { id = result.Value.Id }, result.Value);
    }
}