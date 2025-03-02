using System.ComponentModel.DataAnnotations;
using AviApp.Api.Customers.UpdateCustomer;
using AviApp.Api.Customers;
using AviApp.Api.Customers.DeleteCustomer;
using AviApp.Api.Customers.GetAllCustomers;
using AviApp.Api.Customers.GetCustomerById;
using AviApp.Attributes;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace AviApp.Controllers;


public class CustomerController(IMediator mediator) : AppBaseController
{
    /// <summary>
    /// Get all customers
    /// </summary>
    /// <remarks>Get all customers, no authentication required</remarks>
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


    /// <summary>
    /// Get a customer by id
    /// </summary>
    /// <remarks>Get a customer by id, no authentication required</remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("/api/customers/{id}")]
    [ValidateModelState]
    [SwaggerOperation("GetCustomer")]
    public virtual async Task<IActionResult> GetCustomer([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }

    /// <summary>
    /// Add a new customer
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="customerDto"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="201">New Customer is created</response>
    [HttpPost]
    [Route("/api/customers")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("AddCustomer")]
    [SwaggerResponse(statusCode: 201, type: typeof(CustomerDto), description: "New Customer is created")]
    public virtual async Task<IActionResult> AddCustomer([FromBody]CustomerDto customerDto, CancellationToken cancellationToken)
    {

        var result = await mediator.Send(new CreateCustomerCommand(customerDto), cancellationToken);
        
        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetCustomer), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Errors);
    }
    
    /// <summary>
    /// Update a customer
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="id"></param>
    /// <param name="customerDto"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">customer is updated</response>
    [HttpPut]
    [Route("/api/customers/{id}")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("UpdateCustomer")]
    [Authorize (Roles =  "Admin")]
    public virtual async Task<IActionResult> UpdateCustomer([FromRoute (Name = "id")][Required]int id, [FromBody]CustomerDto? customerDto, CancellationToken cancellationToken)
    {
        
        var result = await mediator.Send(new UpdateCustomerCommand(id, customerDto.CustomerName, customerDto.Phone), cancellationToken);
        return ResultOf(result);
    }

    /// <summary>
    /// Delete a customer
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">Customer is deleted</response>
    [HttpDelete]
    [Route("/api/customers/{id}")]
    [ValidateModelState]
    [SwaggerOperation("DeleteCustomer")]
    [Authorize (Roles =  "Admin")]
    public virtual async Task<IActionResult> DeleteCustomer([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
        return ResultOf(result, successResult: NoContent());
    }
}
