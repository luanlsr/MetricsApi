using MetricsApi.Domain.Entities.Users;
using MetricsApi.Domain.ValueObjects;

namespace MetricsApi.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User?> GetByEmailAsync(Email email);
        Task<bool> EmailExistsAsync(Email email);
    }
}
