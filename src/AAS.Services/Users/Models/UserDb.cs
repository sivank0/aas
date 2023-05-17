#region

using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Services.Users.Models;

public class UserDb
{
    public ID Id { get; set; }
    public String? PhotoPath { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PhoneNumber { get; set; }

    public ID CreatedUserId { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }

    public ID? ModifiedUserId { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public bool IsRemoved { get; set; }

    public UserDb(ID id, String? photoPath, string firstName, string? middleName, string lastName, string email, string passwordHash,
        string phoneNumber, ID createdUserId, DateTime createdDateTimeUtc, ID? modifiedUserId,
        DateTime? modifiedDateTimeUtc, bool isRemoved)
    {
        Id = id;
        PhotoPath = photoPath;
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