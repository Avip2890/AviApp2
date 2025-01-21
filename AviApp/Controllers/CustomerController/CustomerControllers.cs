
using AviApp.Api.Customer.UpdateCustomer;
using AviApp.Api.Customers;
using AviApp.Api.Customers.DeleteCustomer;
using AviApp.Api.Customers.GetAllCustomers;
using AviApp.Api.Customers.GetCustomerById;

using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers.CustomerController;

[ApiController]
[Route("api/customer")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllCustomersQuery(), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateCustomerCommand(customerDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return CreatedAtAction(nameof(GetCustomerById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
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

    [HttpDelete]
    [Route("delete/{id}")]
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
