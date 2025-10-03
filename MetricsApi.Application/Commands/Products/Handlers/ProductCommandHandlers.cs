using MediatR;
using MetricsApi.CrossCutting.Dtos.Producs;
using MetricsApi.Domain.Abstractions;
using MetricsApi.Domain.Entities.Products;
using MetricsApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Products.Handlers
{
    public class ProductCommandHandler :
        IRequestHandler<CreateProductCommand, ProductDto>,
        IRequestHandler<UpdateProductPriceCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(
                 request.Sku,
                 request.Title,
                 request.Price,
                 request.Stock,
                 request.Description
             );

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

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

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null) throw new KeyNotFoundException("Product not found");

            product.Update(request.Title, request.Description, request.Price, request.Stock);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

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

        public async Task<ProductDto> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null) throw new KeyNotFoundException("Product not found");

            product.UpdatePrice(request.NewPrice);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

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

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null) throw new KeyNotFoundException("Product not found");

            _productRepository.Remove(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
