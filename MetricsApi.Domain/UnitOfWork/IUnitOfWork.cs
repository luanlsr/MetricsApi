namespace MetricsApi.Domain.Abstractions
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        /// <summary>
        /// Persiste todas as mudanças rastreadas no contexto.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Inicia uma transação explícita.
        /// </summary>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Efetiva a transação aberta.
        /// </summary>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Desfaz a transação aberta.
        /// </summary>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}