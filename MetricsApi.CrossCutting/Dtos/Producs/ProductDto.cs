using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.CrossCutting.Dtos.Producs
{
    public record ProductDto(
        Guid Id,
        string Sku,
        string Title,
        string? Description,
        decimal Price,
        int Stock,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
