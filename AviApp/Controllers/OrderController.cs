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

    [HttpGet]
    public IActionResult GetAllOrders() => Ok(_orderService.GetAllOrders());

    [HttpGet("{id}")]
    public IActionResult GetOrderById(int id)
    {
        var order = _orderService.GetOrderById(id);
        return order != null ? Ok(order) : NotFound();
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        _orderService.CreateOrder(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, [FromBody] Order updatedOrder)
    {
        _orderService.UpdateOrder(id, updatedOrder);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        _orderService.DeleteOrder(id);
        return NoContent();
    }
}