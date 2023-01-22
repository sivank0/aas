using PMS.Tools.Types.IDs;

namespace AAS.Domain.Users;

public class User
{
    public ID Id { get; }
    public string FirstName { get; }
    public string? MiddleName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PasswordHash { get; }
    public string PhoneNumber { get; }

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
