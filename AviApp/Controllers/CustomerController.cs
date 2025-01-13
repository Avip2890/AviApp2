using AviApp.Api.Customer;
using Microsoft.AspNetCore.Mvc;
using AviApp.Models;
using MediatR;
using AviApp.Api.Customer.CustomerQueries;
using AviApp.Api.Customer.UpdateCustomer;
using AviApp.Api.Customer.DeleteCustomer;

namespace AviApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        try
        {
       
            var customers = await mediator.Send(new GetAllCustomersQuery(), cancellationToken);

         
            return Ok(customers);
        }
        catch (Exception ex)
        {
           
            return BadRequest(new { Message = ex.Message });
        }
    }


    [HttpGet("{id}")]
  
    public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value);
    }


    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new CreateCustomerCommand(customerDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return CreatedAtAction(nameof(GetCustomerById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        if (customerDto.Id != id)
        {
            return BadRequest(new { Message = "Customer ID in URL does not match ID in body." });
        }

        var result = await mediator.Send(new UpdateCustomerCommand(customerDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCustomerCommand(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return NoContent();
    }
}
