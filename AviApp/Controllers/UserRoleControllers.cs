using AviApp.Api.UserRole.CreateUserRole;
using AviApp.Api.UserRole.DeleteUserRole;
using AviApp.Api.UserRole.GetRolesForUser;
using AviApp.Api.UserRole.GetUsersByRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;

[Route("api/usersRoles")]
public class UserRoleControllers (IMediator mediator) : AppBaseController
{
   

    [HttpGet]
    [Route("GetUsersByRole")]
    public async Task<IActionResult> GetUsersByRole([FromQuery] GetUsersByRoleQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetRolesForUser")]
    public async Task<IActionResult> GetRolesForUser([FromQuery] GetRolesForUserQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("createUserRole")]
    
    public async Task<IActionResult> CreateUserRole([FromBody] CreateUserRoleCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete]
    [Route("deleteUserRole")]
    public async Task<IActionResult> DeleteUserRole(int userId, int roleId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteUserRoleCommand( userId,  roleId), cancellationToken);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors);
    }
    
    
}