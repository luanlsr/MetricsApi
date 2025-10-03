using MediatR;
using MetricsApi.CrossCutting.Dtos.Orders;
using MetricsApi.Domain.Entities.Orders.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Orders
{
    public class ChangeOrderStatusCommand : IRequest<OrderDto>
    {
        public Guid OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }
}
