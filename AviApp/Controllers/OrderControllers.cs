using AviApp.Api.Orders.CreateOrder;
using AviApp.Api.Orders.DeleteOrder;
using AviApp.Api.Orders.GetAllOrders;
using AviApp.Api.Orders.GetOrderById;
using AviApp.Api.Orders.UpdateOrder;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;

[Route("api/orders")]
public class OrderController(IMediator mediator) : AppBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllOrdersQuery(), cancellationToken);
        return ResultOf(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto? orderDto, CancellationToken cancellationToken)
    {
        if (orderDto == null)
        {
            return BadRequest("Order data is required.");
        }

        if (!orderDto.OrderMenuItems.Any())
        {
            return BadRequest("Order must contain at least one item.");
        }

        var result = await mediator.Send(new CreateOrderCommand(
            orderDto.CustomerId,
            orderDto.OrderMenuItems.Select(omi => omi.MenuItemId).ToList()
        ), cancellationToken);

        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetOrderById), new { id = result.Value.Id }, result.Value) 
            : BadRequest(result.Errors);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        if (!orderDto.OrderMenuItems.Any())
        {
            return BadRequest("Order must contain at least one item.");
        }

        var result = await mediator.Send(new UpdateOrderCommand(
            id, 
            orderDto.CustomerId, 
            orderDto.OrderDate, 
            orderDto.OrderMenuItems.Select(omi => omi.MenuItemId).ToList()
        ), cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteOrderCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }
}
