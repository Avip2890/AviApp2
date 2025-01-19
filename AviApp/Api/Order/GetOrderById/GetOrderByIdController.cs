using AviApp.Api.Order.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.Order.GetOrderById;

[ApiController]
[Route("api/order/get")]
public class GetOrderByIdController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrderByIdQuery(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value);
    }
}