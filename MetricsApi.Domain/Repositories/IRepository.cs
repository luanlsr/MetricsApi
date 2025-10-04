using MetricsApi.Domain.Specifications;

namespace MetricsApi.Domain.Repositories
{
    /// <summary>
    /// Generic repository abstraction. Keep it minimal to avoid leaking persistence concerns.
    /// Add methods as needed, but prefer domain-specific repository methods on specialized interfaces.
    /// </summary>
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);


        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void Remove(TEntity entity);


        /// <summary>
        /// Find by specification pattern.
        /// </summary>
        Task<IReadOnlyList<TEntity>> FindAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);


        Task<bool> ExistsAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);


        /// <summary>
        /// Optional: expose IQueryable for advanced queries. Use with care: this may couple callers to the ORM.
        /// Implementations can throw NotSupportedException if they don't want to expose queryable.
        /// </summary>
        IQueryable<TEntity> Query();
    }
}
