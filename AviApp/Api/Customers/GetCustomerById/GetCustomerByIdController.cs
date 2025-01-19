
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.Customers.GetCustomerById;

[ApiController]
[Route("api/customer/get/{id}")]
public class GetCustomerByIdController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return Ok(result.Value);
    }
}