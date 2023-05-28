using AAS.Domain.EmailVerifications;
using AAS.Services.EmailVerifications.Models;

namespace AAS.Services.EmailVerifications.Converters;

public static class EmailVerificationsConverter
{
    public static EmailVerification ToEmailVerification(this EmailVerificationDb emailVerificationDb)
    {
        return new EmailVerification(emailVerificationDb.UserId, emailVerificationDb.Token, emailVerificationDb.IsVerified);
    }
}
