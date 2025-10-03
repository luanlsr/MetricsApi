using MediatR;
using MetricsApi.CrossCutting.Dtos.Producs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Products
{
    public record CreateProductCommand(
        string Sku,
        string Title,
        string? Description,
        decimal Price,
        int Stock
    ) : IRequest<ProductDto>;
}
