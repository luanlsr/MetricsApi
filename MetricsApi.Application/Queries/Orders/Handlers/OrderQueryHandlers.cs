using MediatR;
using MetricsApi.Application.Queries.Orders;
using MetricsApi.CrossCutting.Dtos.Orders;
using MetricsApi.Domain.Abstractions;
using MetricsApi.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsApi.Application.Handlers.Orders
{
    public class OrderQueryHandler :
        IRequestHandler<GetOrderByIdQuery, OrderDto>,
        IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
            if (order is null) return null!;

            var items = order.Items.Select(i => new OrderItemDto(i.ProductId, i.Sku, i.UnitPrice, i.Quantity, i.Total)).ToList();

            return new OrderDto(
                order.Id,
                order.UserId,
                order.Status.ToString(),
                items,
                order.CreatedAt,
                order.UpdatedAt
            );
        }

        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);

            return orders.Select(order =>
            {
                var items = order.Items.Select(i => new OrderItemDto(i.ProductId, i.Sku, i.UnitPrice, i.Quantity, i.Total)).ToList();

                return new OrderDto(
                    order.Id,
                    order.UserId,
                    order.Status.ToString(),
                    items,
                    order.CreatedAt,
                    order.UpdatedAt
                );
            }).ToList();
        }
    }
}
