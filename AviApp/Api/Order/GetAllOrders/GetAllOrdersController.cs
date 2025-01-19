using AviApp.Api.Order.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.Order.GetAllOrders;

[ApiController]
[Route("api/order/all")]
public class GetAllOrdersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllOrdersQuery(), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return Ok(result.Value);
    }
}