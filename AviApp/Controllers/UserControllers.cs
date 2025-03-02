using System.ComponentModel.DataAnnotations;
using AviApp.Api.Users.CreateUser;
using AviApp.Api.Users.DeleteUser;
using AviApp.Api.Users.GetAllUsers;
using AviApp.Api.Users.GetUserById;
using AviApp.Api.Users.UpdateUser;
using AviApp.Attributes;
using AviApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AviApp.Controllers;

    public class UserController (IMediator mediator) : AppBaseController
    {
        
        /// <summary>
        /// Get all users
        /// </summary>
        /// <remarks>Get all users, no authentication required</remarks>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/users")]
        [ValidateModelState]
        [SwaggerOperation("GetUsers")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<UserDto>), description: "OK")]
        public virtual async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return ResultOf(result);
        }
        
        /// <summary>
        /// Get a User by id
        /// </summary>
        /// <remarks>Get a User by id, no authentication required</remarks>
        /// <param name="id"></param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/users/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetUser")]
        public virtual async Task<IActionResult> GetUser([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return ResultOf(result);
        }
        
        /// <summary>
        /// Add new user
        /// </summary>
        /// <remarks>Add new user</remarks>
        /// <param name="userDto"></param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <response code="201">New User is created</response>
        [HttpPost]
        [Route("/api/users")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("AddUser")]
        [SwaggerResponse(statusCode: 201, type: typeof(List<UserDto>), description: "New User is created")]
        public virtual async Task<IActionResult> AddUser([FromBody]UserDto userDto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new CreateUserCommand(userDto), cancellationToken);
    
            return !result.IsSuccess ? BadRequest("Failed to create user.") : ResultOf(result, successResult: CreatedAtAction(nameof(GetUser), new { id = result.Value.Id }, result.Value));
        }
        
        /// <summary>
        /// Update User
        /// </summary>
        /// <remarks>Requires **Admin** authentication</remarks>
        /// <param name="id"></param>
        /// <param name="userDto"></param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <response code="200">User is updated</response>
        [HttpPut]
        [Route("/api/users/{id}")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("UpdateUser")]
        public virtual async Task<IActionResult> UpdateUser([FromRoute (Name = "id")][Required]int id, [FromBody]UserDto? userDto, CancellationToken cancellationToken)
        {
            if (id != userDto.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            
            var result = await mediator.Send(new UpdateUserCommand(
                id,
                userDto.Name,
                userDto.Email,
                userDto.Password
                ), cancellationToken);
                
            
            return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
        }
        
        /// <summary>
        /// Delete User
        /// </summary>
        /// <remarks>Requires **Admin** authentication</remarks>
        /// <param name="id"></param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <response code="200">OK</response>
        [HttpDelete]
        [Route("/api/users/{id}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteUser")]
        [Authorize (Roles = "Admin")]
        public virtual async Task<IActionResult> DeleteUser([FromRoute (Name = "id")][Required]int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteUserCommand(id), cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
        }
    }

