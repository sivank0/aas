using AAS.Tools.Types.IDs;

namespace AAS.Domain.EmailVerifications;

public class EmailVerificationBlank
{
    public ID UserId { get; set; }
    public String Token { get; set; }
    public Boolean IsVerified { get; set; }

    public EmailVerificationBlank(ID userId, String token, Boolean isVerified)
    {
        UserId = userId;
        Token = token;
        IsVerified = isVerified;
    }
}
