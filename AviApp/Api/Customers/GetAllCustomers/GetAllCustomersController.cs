using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.Customers.GetAllCustomers;

[ApiController]
[Route("api/customer/get-all")]
public class GetAllCustomersController(IMediator mediator) : ControllerBase
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
}