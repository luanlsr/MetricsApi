using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Domain.Entities.Orders.Events
{
    public record OrderCreated(Guid OrderId, Guid UserId);
    public record OrderItemAdded(Guid OrderId, Guid OrderItemId, Guid ProductId, int Quantity);
    public record OrderItemRemoved(Guid OrderId, Guid OrderItemId);
    public record OrderStatusChanged(Guid OrderId, string NewStatus);
}
