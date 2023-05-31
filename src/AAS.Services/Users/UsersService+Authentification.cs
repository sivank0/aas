using AAS.Domain.EmailVerifications;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Domain.Users.SystemUsers;
using AAS.Domain.Users.UserAccesses;
using AAS.Tools.Managers;
using AAS.Tools.Types.Results;

namespace AAS.Services.Users;

public partial class UsersService : IUsersService
{
    public SystemUser? GetSystemUser(string token)
    {
        DataResult<SystemUser?> systemUserResult = GetSystemUserWithDataResult(token);

        if (!systemUserResult.IsSuccess) return null;

        return systemUserResult.Data;
    }

    private DataResult<SystemUser?> GetSystemUserWithDataResult(string token)
    {
        UserToken? userToken = GetUserToken(token);

        if (userToken is null) return DataResult<SystemUser?>.Fail("Пользователь не существует");

        (User user, EmailVerification emailVerification)? userEmailVerification = GetUserEmailVerification(userToken.UserId);

        if (userEmailVerification is null) return DataResult<SystemUser?>.Fail("Пользователь не найден");

        if (!userEmailVerification.Value.emailVerification.IsVerified)
            return DataResult<SystemUser?>.Fail("Для того, чтобы пользоваться сервисом необходимо подтвердить почту");

        UserRole? userRole = GetUserRoleByUserId(userEmailVerification.Value.user.Id);

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

        (User user, EmailVerification emailVerification)? userEmailVerification = GetUser(email, HashManager.DefinePasswordHash(password));

        if (userEmailVerification is null)
            return DataResult<UserToken?>.Fail("Пользователь не найден, проверьте правильность введенных данных");

        if (!userEmailVerification.Value.emailVerification.IsVerified)
            return DataResult<UserToken?>.Fail("Вы не подтвердили почту");

        UserToken? userToken = UserToken.New(userEmailVerification.Value.user.Id);

        Result savingUserTokenResult = SaveUserToken(userToken);

        if (!savingUserTokenResult.IsSuccess)
            return DataResult<UserToken?>.Fail(savingUserTokenResult.Errors[0].Message);

        Result authentificationResult = Authenticate(userToken.Token);

        if (!authentificationResult.IsSuccess)
            return DataResult<UserToken?>.Fail(authentificationResult.Errors[0].Message);

        return DataResult<UserToken?>.Success(userToken);
    }

    public void LogOut(string token)
    {
        _usersRepository.RemoveToken(token);
    }
}
