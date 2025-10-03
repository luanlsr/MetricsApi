using MetricsApi.Domain.Abstractions;
using MetricsApi.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MetricsApi.Infrastructure.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly DbContext _dbContext;
        private IDbContextTransaction? _currentTransaction;
        private readonly IEventPublisher _eventPublisher;

        public UnitOfWork(DbContext dbContext, IEventPublisher eventPublisher)
        {
            _dbContext = dbContext;
            _eventPublisher = eventPublisher;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (_currentTransaction is null)
            {
                await DispatchDomainEventsAsync();
            }

            return result;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is not null) return;

            _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is null) return;

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync(cancellationToken);

            // Dispara Domain Events somente após commit
            await DispatchDomainEventsAsync();

            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is null) return;

            await _currentTransaction.RollbackAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        private async Task DispatchDomainEventsAsync()
        {
            var entitiesWithEvents = _dbContext.ChangeTracker
                .Entries<Entity<Guid>>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToList();

            foreach (var entity in entitiesWithEvents)
            {
                foreach (var domainEvent in entity.DomainEvents)
                {
                    await _eventPublisher.PublishAsync(domainEvent);
                }
                entity.ClearDomainEvents();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction is not null)
            {
                await _currentTransaction.DisposeAsync();
            }

            await _dbContext.DisposeAsync();
        }
    }
}
