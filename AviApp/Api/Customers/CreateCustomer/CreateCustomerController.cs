
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.Customers;

[ApiController]
[Route("api/customer/create")]
public class CreateCustomerController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new CreateCustomerCommand(customerDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.Error });
        }

        return CreatedAtAction(nameof(CreateCustomer), new { id = result.Value.Id }, result.Value);
    }
}