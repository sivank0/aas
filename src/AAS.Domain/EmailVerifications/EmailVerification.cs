using AAS.Tools.Types.IDs;

namespace AAS.Domain.EmailVerifications;

public class EmailVerification
{
    public ID UserId { get; }
    public String Token { get; }
    public Boolean IsVerified { get; }

    public EmailVerification(ID userId, string token, bool isVerified = false)
    {
        UserId = userId;
        Token = token;
        IsVerified = isVerified;
    }
}
