using System.ComponentModel.DataAnnotations;
using AviApp.Api.Roles.CreateRole;
using AviApp.Api.Roles.DeleteRole;
using AviApp.Api.Roles.GetAllRoles;
using AviApp.Api.Roles.GetRoleById;
using AviApp.Api.Roles.UpdateRole;
using AviApp.Attributes;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AviApp.Controllers;

[Route("api/roles")]

public class RolesController  (IMediator mediator) :AppBaseController
{
    
    /// <summary>
    /// Get all roles
    /// </summary>
    /// <remarks>Get all roles, no authentication required</remarks>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("/api/roles")]
    [ValidateModelState]
    [SwaggerOperation("GetRoles")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<RoleDto>), description: "OK")]
    public virtual async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllRolesQuery(), cancellationToken);
        return ResultOf(result);
    }
    
    /// <summary>
    /// Get a role by id
    /// </summary>
    /// <remarks>Get a role by id, no authentication required</remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("/api/roles/{id}")]
    [ValidateModelState]
    [SwaggerOperation("GetRole")]
    public virtual async Task<IActionResult> GetRole([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetRoleByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }
    
    /// <summary>
    /// Add a new Role
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="roleDto"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="201">New Role is created</response>
    [HttpPost]
    [Route("/api/roles")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("AddRole")]
    [SwaggerResponse(statusCode: 201, type: typeof(List<RoleDto>), description: "New Role is created")]
    [Authorize (Roles = "Admin")]
    public virtual async Task<IActionResult> AddRole([FromBody]RoleDto roleDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateRoleCommand(roleDto.Name),cancellationToken);
        
        if(!result.IsSuccess)
        {
            return BadRequest("Failed to create role.");
        }
        return ResultOf(result, 
            successResult: CreatedAtAction(nameof(GetRole), new { id = result.Value.Id }, result.Value));
    }
    
    /// <summary>
    /// Update role
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="id"></param>
    /// <param name="roleDto"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">Role is updated</response>
    [HttpPut]
    [Route("/api/roles/{id}")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("UpdateRole")]
    [Authorize (Roles = "Admin")]
    public virtual async Task<IActionResult> UpdateRole([FromRoute (Name = "id")][Required]int id, [FromBody]RoleDto? roleDto, CancellationToken cancellationToken)
    {
        if (id != roleDto.Id)
        {
            return BadRequest("Role ID mismatch.");
        }

        
        var result = await mediator.Send(new UpdateRoleCommand(
            id,
            roleDto.Name
            ), cancellationToken);
            
        
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors);
    }
    
    /// <summary>
    /// Delete a role
    /// </summary>
    /// <remarks>Requires **Admin** authentication</remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">Role is deleted</response>
    [HttpDelete]
    [Route("/api/roles/{id}")]
    [ValidateModelState]
    [SwaggerOperation("DeleteRole")]
    [Authorize (Roles = "Admin")]
    public virtual async Task<IActionResult> DeleteRole([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteRoleCommand(id), cancellationToken);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors);
    }
    
}