using MetricsApi.Domain.Common;
using MetricsApi.Domain.Entities.Orders.Enums;
using MetricsApi.Domain.Entities.Orders.Events;

namespace MetricsApi.Domain.Entities.Orders
{
    public class Order : Entity<Guid>
    {
        public Guid UserId { get; private set; }
        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public decimal Total => _items.Sum(i => i.UnitPrice * i.Quantity);
        public OrderStatus Status { get; private set; }

        private Order() { }

        private Order(Guid id, Guid userId)
            : base(id)
        {
            UserId = userId;
            Status = OrderStatus.Created;
        }

        public static Order Create(Guid userId)
        {
            return new Order(Guid.NewGuid(), userId);
        }

        public void AddItem(OrderItem item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var existing = _items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existing != null)
            {
                existing.AddQuantity(item.Quantity);
            }
            else
            {
                _items.Add(item);
                AddDomainEvent(new OrderItemAdded(Id, item.Id, item.ProductId, item.Quantity));
            }

            Touch();
        }

        public void RemoveItem(Guid orderItemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == orderItemId);
            if (item is null) throw new InvalidOperationException("Item not found in order.");

            _items.Remove(item);
            AddDomainEvent(new OrderItemRemoved(Id, item.Id));
            Touch();
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            Touch();
            AddDomainEvent(new OrderStatusChanged(Id, newStatus.ToString()));
        }
    }
}
