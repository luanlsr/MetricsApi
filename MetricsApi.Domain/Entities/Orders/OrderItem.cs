using MetricsApi.Domain.Common;
using MetricsApi.Domain.Entities.Orders.Events;

namespace MetricsApi.Domain.Entities.Orders
{
    public class OrderItem : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public string Sku { get; private set; } = string.Empty;
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        private OrderItem() { } // EF Core

        private OrderItem(Guid id, Guid productId, string sku, decimal unitPrice, int quantity)
            : base(id)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            if (unitPrice < 0) throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));

            ProductId = productId;
            Sku = sku;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        // Factory method público
        public static OrderItem Create(Guid productId, string sku, decimal unitPrice, int quantity)
        {
            return new OrderItem(Guid.NewGuid(), productId, sku, unitPrice, quantity);
        }

        public void AddQuantity(int qty)
        {
            if (qty <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(qty));
            Quantity += qty;
            Touch();
            AddDomainEvent(new OrderItemQuantityChanged(Id, Quantity));
        }

        public decimal Total => UnitPrice * Quantity;
    }
}
