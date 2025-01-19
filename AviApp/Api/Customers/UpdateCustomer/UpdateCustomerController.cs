using Microsoft.AspNetCore.Mvc;
using MediatR;
using AviApp.Models;

namespace AviApp.Api.Customer.UpdateCustomer;

[ApiController]
[Route("api/customer/update/{id}")]
public class UpdateCustomerController(IMediator mediator) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto, CancellationToken cancellationToken = default)
    {
        if (customerDto.Id != id)
        {
            return BadRequest(new { Message = "Customers ID in URL does not match ID in body." });
        }

        var result = await mediator.Send(new UpdateCustomerCommand(customerDto), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value);
    }
}