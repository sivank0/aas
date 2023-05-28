using AAS.Domain.EmailVerifications;
using AAS.Tools.Types.IDs;

namespace AAS.Services.EmailVerifications.Repositories;

public interface IEmailVerificationsRepository
{
    void SaveEmailVerification(EmailVerification emailVerification);
    EmailVerification? GetEmailVerification(ID userId);
}
