namespace MetricsApi.Domain.ValueObjects
{
    public sealed class Email : IEquatable<Email>
    {
        public string Address { get; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Email cannot be empty.", nameof(address));

            Address = address.Trim().ToLowerInvariant();
        }

        public override bool Equals(object? obj) => Equals(obj as Email);
        public bool Equals(Email? other) => other is not null && Address == other.Address;
        public override int GetHashCode() => Address.GetHashCode();
        public override string ToString() => Address;

        public static implicit operator string(Email e) => e.Address;
        public static explicit operator Email(string s) => new Email(s);
    }
}
