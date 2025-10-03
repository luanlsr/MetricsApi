using MediatR;
using MetricsApi.CrossCutting.Dtos.Producs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Queries.Products
{
    public record GetAllProductsQuery() : IRequest<List<ProductDto>>;
}
