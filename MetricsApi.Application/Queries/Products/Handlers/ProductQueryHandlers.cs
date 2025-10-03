using MediatR;
using MetricsApi.Application.Queries.Products;
using MetricsApi.CrossCutting.Dtos.Producs;
using MetricsApi.Domain.Repositories;

namespace MetricsApi.Application.Handlers.Products
{
    public class ProductQueryHandler :
        IRequestHandler<GetProductByIdQuery, ProductDto>,
        IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public ProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null) return null!;

            return new ProductDto(
                product.Id,
                product.Sku,
                product.Title,
                product.Description,
                product.Price,
                product.Stock,
                product.CreatedAt,
                product.UpdatedAt
            );
        }

        public async Task<ProductDto> Handle(GetProductBySkuQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetBySkuAsync(request.Sku);

            if (product is null) return null!;

            return new ProductDto(
                product.Id,
                product.Sku,
                product.Title,
                product.Description,
                product.Price,
                product.Stock,
                product.CreatedAt,
                product.UpdatedAt
            );
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            return products.Select(p => new ProductDto(
                p.Id,
                p.Sku,
                p.Title,
                p.Description,
                p.Price,
                p.Stock,
                p.CreatedAt,
                p.UpdatedAt
            )).ToList();
        }
    }
}
