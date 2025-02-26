using AviApp.Api.Roles.CreateRole;
using AviApp.Api.Roles.DeleteRole;
using AviApp.Api.Roles.GetAllRoles;
using AviApp.Api.Roles.GetRoleById;
using AviApp.Api.Roles.UpdateRole;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;

[Route("api/roles")]

public class RolesController  (IMediator mediator) :AppBaseController
{
    
    [HttpGet]
    [Route("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllRolesQuery(), cancellationToken);
        return ResultOf(result);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetRoleById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetRoleByIdQuery(id), cancellationToken);
        return ResultOf(result);
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateRole([FromBody] RoleDto roleDto, CancellationToken cancellationToken)
    {
        
        var result = await mediator.Send(new CreateRoleCommand(
           
            roleDto.Name
            ), 
            cancellationToken);
        
        if(!result.IsSuccess)
        {
            return BadRequest("Failed to create role.");
        }
        return ResultOf(result, 
            successResult: CreatedAtAction(nameof(GetRoleById), new { id = result.Value.Id }, result.Value));
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto roleDto, CancellationToken cancellationToken)
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
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteRole(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteRoleCommand(id), cancellationToken);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors);
    }
    
}