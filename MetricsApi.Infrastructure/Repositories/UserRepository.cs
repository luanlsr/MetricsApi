using MetricsApi.Domain.Entities.Users;
using MetricsApi.Domain.Repositories;
using MetricsApi.Domain.Specifications;
using MetricsApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MetricsApi.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _users = context.Set<User>();
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _users.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _users.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            await _users.AddAsync(entity, cancellationToken);
        }

        public void Update(User entity)
        {
            _users.Update(entity);
        }

        public void Remove(User entity)
        {
            _users.Remove(entity);
        }

        public async Task<IReadOnlyList<User>> FindAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
        {
            return await _users
                .Where(specification.ToExpression())
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
        {
            return await _users.AnyAsync(specification.ToExpression(), cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
        {
            return await _users.CountAsync(specification.ToExpression(), cancellationToken);
        }

        public IQueryable<User> Query()
        {
            return _users.AsQueryable();
        }

        public async Task<User?> GetByEmailAsync(Domain.ValueObjects.Email email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(Domain.ValueObjects.Email email)
        {
            return await _users.AnyAsync(u => u.Email == email);
        }
    }
}
