using AAS.Configurator;
using AAS.Domain.EmailVerifications;
using AAS.Domain.Users;
using AAS.Tools.Types.Results;
using System.Net;
using System.Net.Mail;

namespace AAS.Services.EmailVerifications.Providers;

public class EmailVerificationsProvider : IEmailVerificationsProvider
{
    public Result SendVerificationMessage(EmailVerification emailVerification, User userForVerification)
    {
        try
        {
            string subject = "Подтверждение Email";
            string emailVerificationUrl = Configurations.EmailVerification.VerificationUrlTemplate.Replace(
                $"{Configurations.EmailVerification.VerificationUrlReplacingValue}", emailVerification.Token
            );
            MailMessage message = new(Configurations.EmailVerification.Login, userForVerification.Email, subject, null);
            message.IsBodyHtml = true;
            message.Body += $"Перейдите по следующей ссылке для подтверждения своей почты<br/><a href='{emailVerificationUrl}'> Подтвердить.</a>";

            using SmtpClient client = new("smtp.gmail.com", 587);

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Configurations.EmailVerification.Login, Configurations.EmailVerification.Password);

            client.Send(message);

            return Result.Success();
        }
        catch
        {
            return Result.Fail("Не удалось отправить сообщение с подтверждением email адреса");
        }
    }
}
