using AviApp.Api.Order.OrderQueries;
using AviApp.Interfaces;
using AviApp.Models;
using MediatR;

namespace AviApp.Api.Order.OrderHandlers
{
    public class GetAllOrdersHandler(IOrderService orderService) : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = await orderService.GetAllOrdersAsync(cancellationToken);

            if (!result.IsSuccess)
            {
                // זריקת שגיאה במקרה של כשל
                throw new Exception(result.Error);
            }

            // המרת ה-Entities ל-DTOs באמצעות LINQ
            return result.Value.Select(order => new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Items = order.Items.Select(item => item.Id).ToList() // העברת מזהי פריטים בלבד
            }).ToList();
        }
    }
}