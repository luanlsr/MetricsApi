using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.CrossCutting.Dtos.Orders
{
    public record OrderDto(
        Guid Id,
        Guid UserId,
        string Status,
        List<OrderItemDto> Items,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
    public record OrderItemDto(Guid ProductId, string Sku, decimal UnitPrice, int Quantity, decimal Total);
}
