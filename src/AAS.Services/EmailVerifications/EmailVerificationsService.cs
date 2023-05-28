using AAS.Configurator;
using AAS.Domain.EmailVerifications;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Services.EmailVerifications.Repositories;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using System.Net;
using System.Net.Mail;

namespace AAS.Services.EmailVerifications;

public class EmailVerificationsService : IEmailVerificationsService
{
    private readonly IUsersService _usersService;
    private readonly IEmailVerificationsRepository _emailVerificationsRepository;

    public EmailVerificationsService(IUsersService usersService, IEmailVerificationsRepository emailVerificationsRepository)
    {
        _usersService = usersService;
        _emailVerificationsRepository = emailVerificationsRepository;
    }

    public Result SendVerificationMessage(ID userId)
    {
        User? existingUser = _usersService.GetUser(userId, includeRemoved: true);

        if (existingUser is null) return Result.Fail("Пользователь не зарегистрирован");

        if (existingUser.IsRemoved) return Result.Fail("Пользователь удален");

        EmailVerification emailVerification = new(userId, Guid.NewGuid().ToString());

        _emailVerificationsRepository.SaveEmailVerification(emailVerification);

        return SendVerificationMessage(emailVerification, existingUser);
    }

    public EmailVerification? GetEmailVerification(ID userId)
    {
        return _emailVerificationsRepository.GetEmailVerification(userId);
    }

    private Result SendVerificationMessage(EmailVerification emailVerification, User userForVerification)
    {
        try
        {
            string subject = "Подтверждение Email";

            MailMessage message = new(Configurations.VerificationEmailAuthorization.Login, userForVerification.Email, subject, null);
            message.IsBodyHtml = true;
            message.Body += $"Перейдите по следующей ссылке для подтверждения своей почты<br/><a href='{emailVerification.Token}'> Подтвердить.</a>";

            using SmtpClient client = new("smtp.gmail.com", 587);

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Configurations.VerificationEmailAuthorization.Login, Configurations.VerificationEmailAuthorization.Password);

            client.Send(message);

            return Result.Success();
        }
        catch
        {
            return Result.Fail("Не удалось отправить сообщение с подтверждением email адреса");
        }
    }
}
