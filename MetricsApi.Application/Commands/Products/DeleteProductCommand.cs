using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Products
{
    public record DeleteProductCommand(Guid Id) : IRequest;
}
