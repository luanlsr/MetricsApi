using MetricsApi.Domain.Common;
using MetricsApi.Domain.Entities.Products.Events;

namespace MetricsApi.Domain.Entities.Products
{
    public class Product : Entity<Guid>
    {
        public string Sku { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        private Product() { }

        private Product(Guid id, string sku, string title, decimal price, int stock, string? description = null)
            : base(id)
        {
            Sku = sku;
            Title = title;
            Price = price;
            Stock = stock;
            Description = description;
        }

        public static Product Create(string sku, string title, decimal price, int stock, string? description = null)
        {
            return new Product(Guid.NewGuid(), sku.Trim(), title.Trim(), price, stock, description?.Trim());
        }

        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
            Touch();
            AddDomainEvent(new ProductPriceUpdated(Id, newPrice));
        }

        public void ChangeStock(int delta)
        {
            var newStock = Stock + delta;
            if (newStock < 0) throw new InvalidOperationException("Stock cannot be negative.");
            Stock = newStock;
            Touch();
            AddDomainEvent(new ProductStockChanged(Id, newStock));
        }

        public void UpdateInfo(string title, string? description)
        {
            if (!string.IsNullOrWhiteSpace(title)) Title = title.Trim();
            Description = description?.Trim();
            Touch();
            AddDomainEvent(new ProductInfoUpdated(Id, Title, Description));
        }
    }
}
