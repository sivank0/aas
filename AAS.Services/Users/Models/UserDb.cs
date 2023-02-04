using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Models;

public class UserDb
{
    public ID Id { get; set; }
    public String FirstName { get; set; }
    public String? MiddleName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public String PasswordHash { get; set; }
    public String PhoneNumber { get; set; }

    public ID CreatedUserId { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }

    public ID? ModifiedUserId { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }

    public UserDb(ID id, string firstName, string? middleName, string lastName, string email, string passwordHash, string phoneNumber, ID createdUserId, DateTime createdDateTimeUtc, ID? modifiedUserId, DateTime? modifiedDateTimeUtc, bool isRemoved)
    {
        Id = id;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        CreatedUserId = createdUserId;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedUserId = modifiedUserId;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }
}
