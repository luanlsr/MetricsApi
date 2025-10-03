using MetricsApi.Domain.Entities.Products;

namespace MetricsApi.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<Product?> GetBySkuAsync(string sku);
    }
}
