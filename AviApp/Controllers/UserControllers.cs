using AviApp.Api.Users.CreateUser;
using AviApp.Api.Users.DeleteUser;
using AviApp.Api.Users.GetAllUsers;
using AviApp.Api.Users.GetUserById;
using AviApp.Api.Users.UpdateUser;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Controllers;

    [Route("api/users")]
    public class UserController (IMediator mediator) : AppBaseController
    {
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return ResultOf(result);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return ResultOf(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new CreateUserCommand(userDto), cancellationToken);
    
            return !result.IsSuccess ? BadRequest("Failed to create user.") : ResultOf(result, successResult: CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value));
        }


        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto, CancellationToken cancellationToken)
        {
            if (id != userDto.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            
            var result = await mediator.Send(new UpdateUserCommand(
                id,
                userDto.UserName,
                userDto.Email,
                userDto.Password
                ), cancellationToken);
                
            
            return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
        }

      
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteUserCommand(id), cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
        }
        
      

    }

