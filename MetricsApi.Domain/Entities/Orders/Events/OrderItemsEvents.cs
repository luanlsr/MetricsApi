using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Domain.Entities.Orders.Events
{
    public record OrderItemQuantityChanged(Guid OrderItemId, int NewQuantity);
}
