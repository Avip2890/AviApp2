using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AviApp.Api.MenuItem.DeleteMenuItem;

[ApiController]
[Route("api/menuitem/delete")]
public class DeleteMenuItemController(IMediator mediator) : ControllerBase
{
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteMenuItemCommand(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.Error });
        }

        return NoContent();
    }
}