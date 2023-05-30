using AAS.Domain.EmailVerifications;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Services.EmailVerifications.Providers;
using AAS.Services.EmailVerifications.Repositories;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Services.EmailVerifications;

public class EmailVerificationsService : IEmailVerificationsService
{
    private readonly IUsersService _usersService;
    private readonly IEmailVerificationsRepository _emailVerificationsRepository;
    private readonly IEmailVerificationsProvider _emailVerificationsProvider;

    public EmailVerificationsService(IUsersService usersService, IEmailVerificationsRepository emailVerificationsRepository, IEmailVerificationsProvider emailVerificationsProvider)
    {
        _usersService = usersService;
        _emailVerificationsRepository = emailVerificationsRepository;
        _emailVerificationsProvider = emailVerificationsProvider;
    }

    public Result SendVerificationMessage(ID userId)
    {
        User? existingUser = _usersService.GetUser(userId, includeRemoved: true);

        if (existingUser is null) return Result.Fail("Пользователь не зарегистрирован");

        if (existingUser.IsRemoved) return Result.Fail("Пользователь удален");

        EmailVerification emailVerification = new(userId, Guid.NewGuid().ToString());

        _emailVerificationsRepository.SaveEmailVerification(emailVerification);

        return _emailVerificationsProvider.SendVerificationMessage(emailVerification, existingUser);
    }

    public Result ResendEmailVerificationMessage(String? userEmail)
    {
        if (String.IsNullOrWhiteSpace(userEmail)) return Result.Fail("Вы не ввели почту");

        User? existingUser = _usersService.GetUser(userEmail);

        if (existingUser is null)
            return Result.Fail("Пользователь не найден, попробуйте пройти процедуру регистрации ещё раз");

        EmailVerification? emailVerification = GetEmailVerification(existingUser.Id);

        if (emailVerification is null) return SendVerificationMessage(existingUser.Id);

        if (emailVerification.IsVerified) return Result.Fail($"Почта: {userEmail} уже подтверждена");

        return _emailVerificationsProvider.SendVerificationMessage(emailVerification, existingUser);
    }

    public EmailVerification? GetEmailVerification(ID userId)
    {
        return _emailVerificationsRepository.GetEmailVerification(userId);
    }

    public Result ConfirmEmail(String? userEmailVerificationToken)
    {
        if (String.IsNullOrWhiteSpace(userEmailVerificationToken))
            return Result.Fail("Произошла ошибка при подтверждении, попробуйте сделать запрос ещё раз");

        (User user, EmailVerification emailVerification)? userEmailVerification = _usersService.GetUserEmailVerification(userEmailVerificationToken);

        if (userEmailVerification is null) return Result.Fail("Пользователь не найден");

        if (!userEmailVerification.Value.emailVerification.IsVerified)
            _emailVerificationsRepository.ConfirmEmail(userEmailVerification.Value.user.Id, userEmailVerification.Value.emailVerification.Token);

        return Result.Success();
    }
}