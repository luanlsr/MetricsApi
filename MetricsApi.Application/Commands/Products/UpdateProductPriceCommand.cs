using MediatR;
using MetricsApi.CrossCutting.Dtos.Producs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Products
{
    public class UpdateProductPriceCommand : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
        public decimal NewPrice { get; set; }
    }
}
