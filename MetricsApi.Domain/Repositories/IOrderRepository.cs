using MetricsApi.Domain.Entities.Orders;

namespace MetricsApi.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
    }
}
