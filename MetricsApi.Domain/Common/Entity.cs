namespace MetricsApi.Domain.Common
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; } = default!;

        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;

        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        protected void Touch() => UpdatedAt = DateTime.UtcNow;

        private readonly List<object> _domainEvents = new();

        /// <summary>
        /// Eventos disparados por esta entidade
        /// </summary>
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(object @event) => _domainEvents.Add(@event);
        protected void RemoveDomainEvent(object @event) => _domainEvents.Remove(@event);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
