using AviApp.Api.Customer;
using Microsoft.AspNetCore.Mvc;
using AviApp.Interfaces;
using AviApp.Domain.Entities;
using AviApp.Models;
using MediatR;

namespace AviApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService, IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        var customers = await customerService.GetAllCustomersAsync(cancellationToken);
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        var customer = await customerService.GetCustomerByIdAsync(id, cancellationToken);
        if (customer == null)
        {
            return NotFound(new { Message = $"Customer with Id {id} not found." });
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customer, CancellationToken cancellationToken = default)
    {
        var command = new CreateCustomerCommand(customer);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        if (customerDto.Id != id)
        {
            return BadRequest(new { Message = "Customer ID in URL does not match ID in body." });
        }

        var updatedCustomer = new Customer
        {
            Id = id,
            CustomerName = customerDto.CustomerName,
            Phone = customerDto.Phone
        };

        var result = await customerService.UpdateCustomerAsync(updatedCustomer, cancellationToken);

        if (result == null)
        {
            return NotFound(new { Message = $"Customer with Id {id} not found." });
        }

        return Ok(result);
    }



}
