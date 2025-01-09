using AviApp.Interfaces;
using AviApp.Mappers;
using AviApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // קבלת כל ההזמנות
    [HttpGet]
    public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken = default)
    {
        var result = await _orderService.GetAllOrdersAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        // מיפוי לתוצאה מסוג OrderDto
        var orderDtos = result.Value.Select(order => order.ToDto());
        return Ok(orderDtos);
    }

    // קבלת הזמנה לפי מזהה
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id, CancellationToken cancellationToken = default)
    {
        var result = await _orderService.GetOrderByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value.ToDto());
    }

    // יצירת הזמנה חדשה
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto, CancellationToken cancellationToken = default)
    {
        // מיפוי מ-DTO ל-Entity
        var menuItems = await _orderService.GetMenuItemsByIdsAsync(orderDto.Items, cancellationToken);
        if (!menuItems.IsSuccess)
        {
            return BadRequest(new { Message = menuItems.Error });
        }

        var orderEntity = orderDto.ToEntity(menuItems.Value);

        var result = await _orderService.CreateOrderAsync(orderEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return CreatedAtAction(nameof(GetOrderById), new { id = result.Value.Id }, result.Value.ToDto());
    }

    // עדכון הזמנה קיימת
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto updatedOrderDto, CancellationToken cancellationToken)
    {
        if (id != updatedOrderDto.Id)
        {
            return BadRequest(new { Message = "Order ID in URL does not match ID in body." });
        }

        // מיפוי מ-DTO ל-Entity
        var menuItems = await _orderService.GetMenuItemsByIdsAsync(updatedOrderDto.Items, cancellationToken);
        if (!menuItems.IsSuccess)
        {
            return BadRequest(new { Message = menuItems.Error });
        }

        var updatedOrderEntity = updatedOrderDto.ToEntity(menuItems.Value);

        var result = await _orderService.UpdateOrderAsync(id, updatedOrderEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value.ToDto());
    }

    // מחיקת הזמנה לפי מזהה
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken = default)
    {
        var result = await _orderService.DeleteOrderAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return NoContent();
    }
}
