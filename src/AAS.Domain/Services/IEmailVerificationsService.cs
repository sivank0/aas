using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Domain.Services;

public interface IEmailVerificationsService
{
    Result ResendEmailVerificationMessage(String? userEmail);
    Result SendVerificationMessage(ID userId);
    Result ConfirmEmail(String? userEmailVerificationToken);
}
