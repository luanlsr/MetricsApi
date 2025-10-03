using MediatR;
using MetricsApi.CrossCutting.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Queries.Orders
{
    public record GetAllOrdersQuery() : IRequest<List<OrderDto>>;
}
