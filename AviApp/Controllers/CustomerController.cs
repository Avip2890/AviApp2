using Microsoft.AspNetCore.Mvc;
using AviApp.Interfaces;
using AviApp.Models;

namespace AviApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        return Ok(_customerService.GetAllCustomers());
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomerById(int id)
    {
        var customer = _customerService.GetCustomerById(id);
        if (customer == null)
        {
            return NotFound(new { Message = $"Customer with Id {id} not found." });
        }

        return Ok(customer);
    }

    [HttpPost]
    public IActionResult CreateCustomer(Customer customer)
    {
        var newCustomer = _customerService.CreateCustomer(customer);
        return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.Id }, newCustomer);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, Customer updatedCustomer)
    {
        var customer = _customerService.UpdateCustomer(id, updatedCustomer);
        if (customer == null)
        {
            return NotFound(new { Message = $"Customer with Id {id} not found." });
        }

        return Ok(customer);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var result = _customerService.DeleteCustomer(id);
        if (!result)
        {
            return NotFound(new { Message = $"Customer with Id {id} not found." });
        }

        return NoContent();
    }
}