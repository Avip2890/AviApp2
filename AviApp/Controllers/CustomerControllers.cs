using AviApp.Api.Customer.UpdateCustomer;
using AviApp.Api.Customers;
using AviApp.Api.Customers.DeleteCustomer;
using AviApp.Api.Customers.GetAllCustomers;
using AviApp.Api.Customers.GetCustomerById;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;


[Route("api/customers")]
public class CustomerController(IMediator mediator) : AppBaseController
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken)
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
        var result = await mediator.Send(new CreateCustomerCommand(customerDto), cancellationToken);

        return ResultOf(result, 
            successResult: CreatedAtAction(nameof(GetCustomerById), new { id = result.Value.Id }, result.Value));
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
    {
        
        var result = await mediator.Send(new UpdateCustomerCommand(customerDto), cancellationToken);
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
