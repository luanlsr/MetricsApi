using MetricsApi.Domain.Common;
using MetricsApi.Domain.Entities.Users.Events;
using MetricsApi.Domain.ValueObjects;

namespace MetricsApi.Domain.Entities.Users
{
    public class User : Entity<Guid>
    {
        public PersonName Name { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public bool Active { get; private set; }

        private User() { }

        private User(Guid id, PersonName name, Email email)
            : base(id)
        {
            Name = name;
            Email = email;
            Active = true;
            AddDomainEvent(new UserCreated(id, email));
        }

        public static User Create(PersonName name, Email email)
        {
            return new User(Guid.NewGuid(), name, email);
        }

        public void ChangeName(PersonName newName)
        {
            if (newName is null) throw new ArgumentNullException(nameof(newName));
            Name = newName;
            Touch();
            AddDomainEvent(new UserNameChanged(Id, Name));
        }

        public void UpdateEmail(Email newEmail)
        {
            if (Email.Equals(newEmail)) return;
            Email = newEmail;
            Touch();
            AddDomainEvent(new UserEmailChanged(Id, Email));
        }

        public void Deactivate()
        {
            Active = false;
            Touch();
            AddDomainEvent(new UserDeactivated(Id));
        }

        public void Activate()
        {
            Active = true;
            Touch();
            AddDomainEvent(new UserActivated(Id));
        }
    }
}
