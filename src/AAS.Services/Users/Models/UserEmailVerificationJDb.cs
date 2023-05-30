using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Models;

public class UserEmailVerificationJDb
{
    public ID Id { get; set; }
    public String? PhotoPath { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PhoneNumber { get; set; }

    public ID UserCreatedUserId { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }

    public ID? ModifiedUserId { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public bool IsRemoved { get; set; }

    public String Token { get; set; }
    public Boolean IsVerified { get; set; }
    public DateTime EmailVerificatonCreatedDateTimeUtc { get; set; }

    public UserEmailVerificationJDb(ID id, string? photoPath, string firstName, string? middleName, string lastName, string email, string passwordHash, string phoneNumber,
        ID userCreatedUserId, DateTime createdDateTimeUtc, ID? modifiedUserId, DateTime? modifiedDateTimeUtc, bool isRemoved, string token, bool isVerified,
        DateTime emailVerificatonCreatedDateTimeUtc)
    {
        Id = id;
        PhotoPath = photoPath;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        UserCreatedUserId = userCreatedUserId;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedUserId = modifiedUserId;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
        Token = token;
        IsVerified = isVerified;
        EmailVerificatonCreatedDateTimeUtc = emailVerificatonCreatedDateTimeUtc;
    }
}
