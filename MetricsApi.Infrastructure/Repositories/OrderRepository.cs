using MetricsApi.Domain.Entities.Orders;
using MetricsApi.Domain.Entities.Products;
using MetricsApi.Domain.Repositories;
using MetricsApi.Domain.Specifications;
using MetricsApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MetricsApi.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Order> _orders;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _orders = dbContext.Set<Order>();
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _orders
                .Include(o => o.Items)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Order entity, CancellationToken cancellationToken = default)
        {
            await _orders.AddAsync(entity, cancellationToken);
        }

        public void Update(Order entity)
        {
            _orders.Update(entity);
        }

        public void Remove(Order entity)
        {
            _orders.Remove(entity);
        }

        public async Task<IReadOnlyList<Order>> FindAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default)
        {
            return await _orders
                .Include(o => o.Items)
                .Where(specification.ToExpression())
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default)
        {
            return await _orders.AnyAsync(specification.ToExpression(), cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default)
        {
            return await _orders.CountAsync(specification.ToExpression(), cancellationToken);
        }


        public IQueryable<Order> Query()
        {
            return _orders.Include(o => o.Items).AsQueryable();
        }
    }
}
