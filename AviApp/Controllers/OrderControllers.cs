using System.ComponentModel.DataAnnotations;
using AviApp.Api.Orders.CreateOrder;
using AviApp.Api.Orders.DeleteOrder;
using AviApp.Api.Orders.GetAllOrders;
using AviApp.Api.Orders.GetOrderById;
using AviApp.Api.Orders.UpdateOrder;
using AviApp.Attributes;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AviApp.Controllers;


public class OrderController(IMediator mediator) : AppBaseController
{
    /// <summary>
    /// Get all orders
    /// </summary>
    /// <remarks>Get all orders, no authentication required</remarks>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("/api/orders")]
    [ValidateModelState]
    [SwaggerOperation("GetOrders")]
    public virtual async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllOrdersQuery(), cancellationToken);
        return ResultOf(result);
    }

    /// <summary>
    /// Get an order by id
    /// </summary>
    /// <remarks>Get an order by id, no authentication required</remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("/api/orders/{id}")]
    [ValidateModelState]
    [SwaggerOperation("GetOrder")]
    public virtual async Task<IActionResult> GetOrder([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }

    /// <summary>
    /// Add a new order
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="orderDto"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="201">New Order Created</response>
    [HttpPost]
    [Route("/api/orders")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("AddOrder")]
    [SwaggerResponse(statusCode: 201, type: typeof(List<OrderDto>), description: "New Order Created")]
    [Authorize (Roles = "Admin")]
    public virtual async Task<IActionResult> AddOrder([FromBody]OrderDto orderDto, CancellationToken cancellationToken)
    {
        if (orderDto == null)
        {
            return BadRequest("Order data is required.");
        }

        if (!orderDto.OrderMenuItems.Any())
        {
            return BadRequest("Order must contain at least one item.");
        }

        var result = await mediator.Send(new CreateOrderCommand(new OrderDto()), cancellationToken);

        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetOrder), new { id = result.Value.Id }, result.Value) 
            : BadRequest(result.Errors);
    }

    /// <summary>
    /// Update an order
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="id"></param>
    /// <param name="orderDto"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpPut]
    [Route("/api/orders/{id}")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("UpdateOrder")]
    [Authorize (Roles = "Admin")]
    public virtual async Task<IActionResult> UpdateOrder([FromRoute (Name = "id")][Required]int id, [FromBody]OrderDto? orderDto, CancellationToken cancellationToken)
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

    /// <summary>
    /// Delete an order
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">Order is deleted</response>
    [HttpDelete]
    [Route("/api/orders/{id}")]
    [ValidateModelState]
    [SwaggerOperation("DeleteOrder")]
    [Authorize (Roles = "Admin")]
    public virtual async Task<IActionResult> DeleteOrder([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteOrderCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }
}
