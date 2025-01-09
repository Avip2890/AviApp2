using Microsoft.AspNetCore.Mvc;
using AviApp.Interfaces;
using AviApp.Models;
using MediatR;
using AviApp.Mappers;

namespace AviApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService, IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        var result = await customerService.GetAllCustomersAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return Ok(result.Value); 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        var result = await customerService.GetCustomerByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value); 
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        // המרת CustomerDto ל- Customer
        var customerEntity = customerDto.ToEntity();

        var result = await customerService.CreateCustomerAsync(customerEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        // המרת Customer חזרה ל- CustomerDto
        return CreatedAtAction(nameof(GetCustomerById), new { id = result.Value.Id }, result.Value.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        if (customerDto.Id != id)
        {
            return BadRequest(new { Message = "Customer ID in URL does not match ID in body." });
        }

        // המרת CustomerDto ל- Customer
        var customerEntity = customerDto.ToEntity();

        var result = await customerService.UpdateCustomerAsync(customerEntity, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        // המרת Customer חזרה ל- CustomerDto
        return Ok(result.Value.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
    {
        var result = await customerService.DeleteCustomerAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return NoContent();
    }
}
