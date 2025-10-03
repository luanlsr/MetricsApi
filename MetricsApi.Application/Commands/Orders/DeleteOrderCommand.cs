using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Orders
{
    public record DeleteOrderCommand(Guid Id) : IRequest;
}
