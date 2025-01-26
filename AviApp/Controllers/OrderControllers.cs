using AviApp.Api.Order.CreateOrder;
using AviApp.Api.Order.DeleteOrder;
using AviApp.Api.Order.GetAllOrders;
using AviApp.Api.Order.GetOrderById;
using AviApp.Api.Order.UpdateOrder;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;


[Route("api/orders")]
public class OrderController(IMediator mediator) : AppBaseController
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllOrdersQuery(), cancellationToken);
        return ResultOf(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateOrderCommand(orderDto), cancellationToken);
        return ResultOf(result, 
            successResult: CreatedAtAction(nameof(GetOrderById), new { id = result.Value.Id }, result.Value));
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateOrderCommand(id, orderDto), cancellationToken);
        return ResultOf(result);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteOrderCommand(id), cancellationToken);
        return ResultOf(result, successResult: NoContent());
    }
}
