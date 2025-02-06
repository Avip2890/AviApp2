using AviApp.Results;
using MediatR;

namespace AviApp.Api.Orders.DeleteOrder;

public record DeleteOrderCommand(int Id) : IRequest<Result<Deleted>>;