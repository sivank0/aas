using AAS.Configurator;
using AAS.Domain.EmailVerifications;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Domain.Users.SystemUsers;
using AAS.Domain.Users.UserAccesses;
using AAS.Services.Users.Repositories;
using AAS.Tools.Managers;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Services.Users;

public class UsersAuthentificationService : IUsersAuthentificationService
{
    private readonly IUsersService _usersService;
    private readonly IUsersRepository _usersRepository;
    private readonly IEmailVerificationsService _emailVerificationsService;

    public UsersAuthentificationService(IUsersService usersService, IUsersRepository usersRepository, IEmailVerificationsService emailVerificationsService)
    {
        _usersService = usersService;
        _usersRepository = usersRepository;
        _emailVerificationsService = emailVerificationsService;
    }

    public SystemUser? GetSystemUser(string token)
    {
        DataResult<SystemUser?> systemUserResult = GetSystemUserWithDataResult(token);

        if (!systemUserResult.IsSuccess) return null;

        return systemUserResult.Data;
    }

    private DataResult<SystemUser?> GetSystemUserWithDataResult(string token)
    {
        UserToken? userToken = _usersService.GetUserToken(token);

        if (userToken is null) return DataResult<SystemUser?>.Fail("Пользователь не существует");

        (User user, EmailVerification emailVerification)? userEmailVerification = _usersService.GetUserEmailVerification(userToken.UserId);

        if (userEmailVerification is null) return DataResult<SystemUser?>.Fail("Пользователь не найден");

        if (!userEmailVerification.Value.emailVerification.IsVerified)
            return DataResult<SystemUser?>.Fail("Для того, чтобы пользоваться сервисом необходимо подтвердить почту");

        UserRole? userRole = _usersService.GetUserRoleByUserId(userEmailVerification.Value.user.Id);

        if (userRole is null) return DataResult<SystemUser?>.Fail("Вам не назначена роль");

        UserAccess userAccess = new UserAccess(userRole.AccessPolicies);

        return DataResult<SystemUser?>.Success(new SystemUser(userEmailVerification.Value.user, userAccess));
    }

    public Result Authenticate(string token)
    {
        DataResult<SystemUser?> systemUserResult = GetSystemUserWithDataResult(token);

        if (!systemUserResult.IsSuccess) return Result.Fail(systemUserResult.Errors[0].Message);

        return Result.Success();
    }

    public DataResult<UserToken?> LogIn(string? email, string? password)
    {
        if (string.IsNullOrWhiteSpace(email))
            return DataResult<UserToken?>.Fail("Не введен Email");

        if (string.IsNullOrWhiteSpace(password))
            return DataResult<UserToken?>.Fail("Не введен пароль");

        (User user, EmailVerification emailVerification)? userEmailVerification = _usersService.GetUser(email, HashManager.DefinePasswordHash(password));

        if (userEmailVerification is null)
            return DataResult<UserToken?>.Fail("Пользователь не найден, проверьте правильность введенных данных");

        if (!userEmailVerification.Value.emailVerification.IsVerified)
            return DataResult<UserToken?>.Fail("Вы не подтвердили почту");

        UserToken? userToken = UserToken.New(userEmailVerification.Value.user.Id);

        Result savingUserTokenResult = _usersService.SaveUserToken(userToken);

        if (!savingUserTokenResult.IsSuccess)
            return DataResult<UserToken?>.Fail(savingUserTokenResult.Errors[0].Message);

        Result authentificationResult = Authenticate(userToken.Token);

        if (!authentificationResult.IsSuccess)
            return DataResult<UserToken?>.Fail(authentificationResult.Errors[0].Message);

        return DataResult<UserToken?>.Success(userToken);
    }

    public DataResult<UserToken?> RegisterUser(UserRegistrationBlank userRegistrationBlank)
    {
        if (string.IsNullOrWhiteSpace(userRegistrationBlank.Email))
            return DataResult<UserToken?>.Fail("Не введен Email");

        if (string.IsNullOrWhiteSpace(userRegistrationBlank.FirstName))
            return DataResult<UserToken?>.Fail("Не введено имя");

        if (string.IsNullOrWhiteSpace(userRegistrationBlank.LastName))
            return DataResult<UserToken?>.Fail("Не введена фамилия");

        if (string.IsNullOrWhiteSpace(userRegistrationBlank.PhoneNumber))
            return DataResult<UserToken?>.Fail("Не введнен номер телефона");

        if (string.IsNullOrWhiteSpace(userRegistrationBlank.Password))
            return DataResult<UserToken?>.Fail("Не введнен пароль");

        if (string.IsNullOrWhiteSpace(userRegistrationBlank.RePassword))
            return DataResult<UserToken?>.Fail("Не введнен повторно пароль");

        if (userRegistrationBlank.Password != userRegistrationBlank.RePassword)
            return DataResult<UserToken?>.Fail("Пароли не совпадают");

        User? existingUser = _usersService.GetUser(userRegistrationBlank.Email);

        if (existingUser is not null) return DataResult<UserToken?>.Fail("Пользователь с такой почтой существует");

        userRegistrationBlank.Id = ID.New();

        _usersRepository.RegisterUser(userRegistrationBlank, Configurations.BackOffice.DefaultRoleId);
        _emailVerificationsService.SendVerificationMessage(userRegistrationBlank.Id.Value);

        UserToken? userToken = UserToken.New(userRegistrationBlank.Id!.Value);

        Result savingUserTokenResult = _usersService.SaveUserToken(userToken);

        if (!savingUserTokenResult.IsSuccess)
            return DataResult<UserToken?>.Fail(savingUserTokenResult.Errors[0].Message);

        return DataResult<UserToken?>.Success(userToken);
    }

    public void LogOut(string token)
    {
        _usersRepository.RemoveToken(token);
    }
}
