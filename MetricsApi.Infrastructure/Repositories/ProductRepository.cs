using MetricsApi.Domain.Entities.Products;
using MetricsApi.Domain.Repositories;
using MetricsApi.Domain.Specifications;
using MetricsApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MetricsApi.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Product> _products;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _products = dbContext.Set<Product>();
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _products.FindAsync(new object?[] { id }, cancellationToken);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _products.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Product entity, CancellationToken cancellationToken = default)
        {
            await _products.AddAsync(entity, cancellationToken);
        }

        public void Update(Product entity)
        {
            _products.Update(entity);
        }

        public void Remove(Product entity)
        {
            _products.Remove(entity);
        }

        public async Task<IReadOnlyList<Product>> FindAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
        {
            return await _products
                .Where(specification.ToExpression())
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
        {
            return await _products.AnyAsync(specification.ToExpression(), cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
        {
            return await _products.CountAsync(specification.ToExpression(), cancellationToken);
        }

        public IQueryable<Product> Query()
        {
            return _products.AsQueryable();
        }

        // Métodos específicos do ProductRepository
        public async Task<Product?> GetBySkuAsync(string sku)
        {
            return await _products.FirstOrDefaultAsync(p => p.Sku == sku);
        }
    }
}
