#region

using System.Text.Json.Serialization;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Domain.Users;

public class User
{
    public ID Id { get; }
    public string FirstName { get; }
    public string? MiddleName { get; }
    public string LastName { get; }

    public string FullName => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{LastName} {FirstName}"
        : $"{LastName} {FirstName} {MiddleName}";

    public string Email { get; }
    public string PhoneNumber { get; }

    [JsonIgnore] public string PasswordHash { get; }

    public User(ID id, string firstName, string? middleName, string lastName, string email, string passwordHash,
        string phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
    }
}