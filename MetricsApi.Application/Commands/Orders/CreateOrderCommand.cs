using MediatR;
using MetricsApi.CrossCutting.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Orders
{
    public record CreateOrderCommand(Guid UserId, List<OrderItemDto> Items) : IRequest<OrderDto>;
}
