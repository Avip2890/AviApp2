using AviApp.Api.Customers.UpdateCustomer;
using AviApp.Api.Customers;
using AviApp.Api.Customers.DeleteCustomer;
using AviApp.Api.Customers.GetAllCustomers;
using AviApp.Api.Customers.GetCustomerById;
using AviApp.Attributes;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AviApp.Controllers;


public class CustomerController(IMediator mediator) : AppBaseController
{
    /// <summary>
    /// Get all customers
    /// </summary>
    /// <remarks>Get all customers</remarks>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("/api/customers")]
    [ValidateModelState]
    [SwaggerOperation("GetCustomers")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<CustomerDto>), description: "OK")]
    public virtual async Task<IActionResult> GetCustomers(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllCustomersQuery(), cancellationToken);
        return ResultOf(result);
    }


    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateCustomerCommand(customerDto.CustomerName, customerDto.Phone), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest("Failed to create customer.");
        }
        return ResultOf(result, successResult: Created("customer", result.Value));
    }


    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
    {
        
        var result = await mediator.Send(new UpdateCustomerCommand(id, customerDto.CustomerName, customerDto.Phone), cancellationToken);
        return ResultOf(result);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
        return ResultOf(result, successResult: NoContent());
    }
}
