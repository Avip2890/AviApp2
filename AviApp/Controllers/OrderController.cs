using AviApp.Interfaces;
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
        var orders = await _orderService.GetAllOrdersAsync(cancellationToken);
        return Ok(orders);
    }

    // קבלת הזמנה לפי מזהה
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id, CancellationToken cancellationToken = default)
    {
        var order = await _orderService.GetOrderByIdAsync(id, cancellationToken);
        return order != null ? Ok(order) : NotFound(new { Message = $"Order with Id {id} not found." });
    }

    // יצירת הזמנה חדשה
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto, CancellationToken cancellationToken = default)
    {
        var createdOrder = await _orderService.CreateOrderAsync(orderDto, cancellationToken);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
    }

    // עדכון הזמנה קיימת
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto updatedOrderDto, CancellationToken cancellationToken = default)
    {
        if (id != updatedOrderDto.Id)
        {
            return BadRequest(new { Message = "Order ID in URL does not match ID in body." });
        }

        var updatedOrder = await _orderService.UpdateOrderAsync(id, updatedOrderDto, cancellationToken);
        return updatedOrder != null 
            ? Ok(updatedOrder) 
            : NotFound(new { Message = $"Order with Id {id} not found." });
    }

    // מחיקת הזמנה לפי מזהה
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken = default)
    {
        var result = await _orderService.DeleteOrderAsync(id, cancellationToken);
        return result 
            ? NoContent() 
            : NotFound(new { Message = $"Order with Id {id} not found." });
    }
}
