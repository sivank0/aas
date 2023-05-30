using AAS.Domain.EmailVerifications;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace AAS.BackOffice.Areas.EmailVerifications;

public class EmailVerificationsController : Controller
{
    private readonly IEmailVerificationsService _emailVerificationsService;
    private readonly IUsersService _usersService;

    public EmailVerificationsController(IEmailVerificationsService emailVerificationsService, IUsersService usersService)
    {
        _emailVerificationsService = emailVerificationsService;
        _usersService = usersService;
    }

    [HttpGet("email_verificaton/resend_email_verification_message")]
    public Result ResendEmailVerificationMessage(String? userEmail)
    {
        return _emailVerificationsService.ResendEmailVerificationMessage(userEmail);
    }

    [HttpGet("email_verifications/confirm_email")]
    public Result ConfirmEmail(String? userEmailVerificationToken)
    {
        return _emailVerificationsService.ConfirmEmail(userEmailVerificationToken);
    }

    [HttpGet("email_verifications/get_user_email_verification")]
    public Object? GetEmailVerificationUser(String userEmailVerificationToken)
    {
        (User user, EmailVerification emailVerification)? userEmailVerification = _usersService.GetUserEmailVerification(userEmailVerificationToken);

        if (userEmailVerification is null) return null;

        return new { User = userEmailVerification.Value.user, EmailVerification = userEmailVerification.Value.emailVerification };
    }
}
