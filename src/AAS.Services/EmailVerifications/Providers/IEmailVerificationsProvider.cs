using AAS.Domain.EmailVerifications;
using AAS.Domain.Users;
using AAS.Tools.Types.Results;

namespace AAS.Services.EmailVerifications.Providers;

public interface IEmailVerificationsProvider
{
    Result SendVerificationMessage(EmailVerification emailVerification, User userForVerification);
}
