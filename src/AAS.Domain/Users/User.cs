using AAS.Tools.Types.IDs;
using System.Text.Json.Serialization;
using File = AAS.Domain.Files.File;

namespace AAS.Domain.Users;

public class User
{
    public ID Id { get; }
    public File? Photo { get; }
    public string FirstName { get; }
    public string? MiddleName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public Boolean IsRemoved { get; }

    public string FullName => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{LastName} {FirstName}"
        : $"{LastName} {FirstName} {MiddleName}";

    [JsonIgnore] public string PasswordHash { get; }

    public User(ID id, File? photo, string firstName, string? middleName, string lastName, string email,
        string phoneNumber, string passwordHash, Boolean isRemoved)
    {
        Id = id;
        Photo = photo;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        IsRemoved = isRemoved;
    }
}