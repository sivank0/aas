using AAS.Tools.Types.IDs;

namespace AAS.Services.EmailVerifications.Models;

public class EmailVerificationDb
{
    public ID UserId { get; set; }
    public String Token { get; set; }
    public Boolean IsVerified { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }

    public EmailVerificationDb(ID userId, string token, bool isVerified, DateTime createdDateTimeUtc)
    {
        UserId = userId;
        Token = token;
        IsVerified = isVerified;
        CreatedDateTimeUtc = createdDateTimeUtc;
    }
}
