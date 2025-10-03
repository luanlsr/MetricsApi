using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Domain.Entities.Products.Events
{
    public record ProductCreated(Guid ProductId, string Sku, string Title);
    public record ProductPriceUpdated(Guid ProductId, decimal NewPrice);
    public record ProductStockChanged(Guid ProductId, int NewStock);
    public record ProductInfoUpdated(Guid ProductId, string Title, string? Description);
}
