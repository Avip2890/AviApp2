using AviApp.Results;
using MediatR;

namespace AviApp.Api.Order.DeleteOrder;

public record DeleteOrderCommand(int Id) : IRequest<Result<Deleted>>;