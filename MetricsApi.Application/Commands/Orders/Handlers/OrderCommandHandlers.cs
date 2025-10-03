using MediatR;
using MetricsApi.CrossCutting.Dtos.Orders;
using MetricsApi.Domain.Abstractions;
using MetricsApi.Domain.Entities.Orders;
using MetricsApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Orders.Handlers
{
    public class OrderCommandHandler :
       IRequestHandler<CreateOrderCommand, OrderDto>,
       IRequestHandler<AddOrderItemCommand, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Order.Create(request.UserId);

            foreach (var itemDto in request.Items)
            {
                var item = OrderItem.Create(itemDto.ProductId, itemDto.Sku, itemDto.UnitPrice, itemDto.Quantity);
                order.AddItem(item);
            }

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToDto(order);
        }

        public async Task<OrderDto> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order is null) throw new KeyNotFoundException("Order not found");

            order.ChangeStatus(request.NewStatus);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToDto(order);
        }


        public async Task<OrderDto> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order is null) throw new KeyNotFoundException("Order not found");

            var item = OrderItem.Create(request.Item.ProductId, request.Item.Sku, request.Item.UnitPrice, request.Item.Quantity);
            order.AddItem(item);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToDto(order);
        }

        private static OrderDto MapToDto(Order order)
        {
            var items = order.Items
                             .Select(i => new OrderItemDto(i.ProductId, i.Sku, i.UnitPrice, i.Quantity, i.Total))
                             .ToList();

            return new OrderDto(
                order.Id,
                order.UserId,
                order.Status.ToString(),
                items,
                order.CreatedAt,
                order.UpdatedAt
            );
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
            if (order is null) throw new KeyNotFoundException("Order not found");

            _orderRepository.Remove(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
