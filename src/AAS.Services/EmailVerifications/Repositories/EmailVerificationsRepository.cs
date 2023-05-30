using AAS.Domain.EmailVerifications;
using AAS.Services.EmailVerifications.Converters;
using AAS.Services.EmailVerifications.Models;
using AAS.Services.EmailVerifications.Repositories.Queries;
using AAS.Tools.DB;
using AAS.Tools.Types.IDs;

namespace AAS.Services.EmailVerifications.Repositories;

public class EmailVerificationsRepository : NpgSqlRepository, IEmailVerificationsRepository
{
    public EmailVerificationsRepository(string connectionString) : base(connectionString)
    {
    }

    public void SaveEmailVerification(EmailVerification emailVerification)
    {
        SqlParameter[] parameters =
        {
            new("p_userid", emailVerification.UserId),
            new("p_token", emailVerification.Token),
            new("p_isverified", emailVerification.IsVerified),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.EmailVerifications_Save, parameters);
    }

    public EmailVerification? GetEmailVerification(ID userId)
    {
        SqlParameter[] parameters =
        {
            new("p_userid", userId)
        };

        return Get<EmailVerificationDb?>(Sql.EmailVerifications_GetByUserId, parameters)?.ToEmailVerification();
    }

    public void ConfirmEmail(ID userId, String userEmailVerificationToken)
    {
        SqlParameter[] parameters =
{
            new("p_userid", userId),
            new("p_token", userEmailVerificationToken),
        };

        Execute(Sql.EmailVerifications_ConfirmEmail, parameters);
    }
}
