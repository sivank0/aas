using AAS.Tools.Types.IDs;

namespace AAS.Domain.Users;

public class User
{
    public ID Id { get; }
    public String FirstName { get; }
    public String? MiddleName { get; }
    public String LastName { get; }
    public String FullName => String.IsNullOrWhiteSpace(MiddleName) ? LastName + FirstName : LastName + FirstName + MiddleName;
    public String Email { get; }
    public String PasswordHash { get; }
    public String PhoneNumber { get; }

    public User(ID id, string firstName, string? middleName, string lastName, string email, string passwordHash, string phoneNumber)
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
