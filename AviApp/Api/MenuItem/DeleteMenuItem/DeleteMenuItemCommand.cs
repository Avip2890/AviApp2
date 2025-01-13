using AviApp.Results;
using MediatR;
namespace AviApp.Api.MenuItem.DeleteMenuItem;





public record DeleteMenuItemCommand(int Id) : IRequest<Result<bool>>;