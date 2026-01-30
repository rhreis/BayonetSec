using System.Text.RegularExpressions;

namespace BayonetSec.Domain.ValueObjects;

public class Email
{
    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");

        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format");

        return new Email(email);
    }

    private static bool IsValidEmail(string email)
    {
        // Simple regex for validation
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj) => obj is Email email && Value == email.Value;

    public override int GetHashCode() => Value.GetHashCode();
}