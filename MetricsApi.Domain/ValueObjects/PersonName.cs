namespace MetricsApi.Domain.ValueObjects
{
    public sealed class PersonName : IEquatable<PersonName>
    {
        public string FirstName { get; }
        public string? MiddleName { get; }
        public string LastName { get; }


        public string FullName => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{FirstName} {LastName}"
        : $"{FirstName} {MiddleName} {LastName}";


        public PersonName(string firstName, string lastName, string? middleName = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));


            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));


            FirstName = firstName.Trim();
            MiddleName = string.IsNullOrWhiteSpace(middleName) ? null : middleName.Trim();
            LastName = lastName.Trim();
        }


        public override bool Equals(object? obj) => Equals(obj as PersonName);
        public bool Equals(PersonName? other)
        => other is not null
        && FirstName == other.FirstName
        && LastName == other.LastName
        && MiddleName == other.MiddleName;


        public override int GetHashCode() => HashCode.Combine(FirstName, MiddleName, LastName);
        public override string ToString() => FullName;
    }
}
