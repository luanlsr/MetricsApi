public sealed class PersonName : IEquatable<PersonName>
{
    public string FirstName { get; }
    public string? MiddleName { get; }
    public string LastName { get; }

    public string FullName => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{FirstName} {LastName}"
        : $"{FirstName} {MiddleName} {LastName}";

    private PersonName(string firstName, string lastName, string? middleName = null)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        MiddleName = string.IsNullOrWhiteSpace(middleName) ? null : middleName.Trim();
    }

    public static PersonName FromFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Name cannot be empty.", nameof(fullName));

        var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 1)
        {
            return new PersonName(parts[0], parts[0]);
        }
        else if (parts.Length == 2)
        {
            return new PersonName(parts[0], parts[1]);
        }
        else
        {
            var firstName = parts[0];
            var lastName = parts[^1]; 
            var middleName = string.Join(' ', parts[1..^1]);
            return new PersonName(firstName, lastName, middleName);
        }
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
