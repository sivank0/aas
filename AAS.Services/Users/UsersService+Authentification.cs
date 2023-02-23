using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Domain.Users.SystemUsers;
using AAS.Domain.Users.UserAccesses;
using AAS.Tools.Managers;
using AAS.Tools.Types.Results;

namespace AAS.Services.Users;

public partial class UsersService : IUsersService // + Authentification
{
    public SystemUser? GetSystemUser(String token)
    {
        UserToken? userToken = GetUserToken(token);

        if (userToken is null) return null;

        User? user = GetUser(userToken.UserId);

        if (user is null) return null;

        UserRole? userRole = GetUserRole(user.Id);

        if (userRole is null) return null;

        UserAccess userAccess = new UserAccess(userRole.AccessPolicies);

        return new SystemUser(user, userAccess);
    }

    public Result Authenticate(String token)
    {
        SystemUser? systemUser = GetSystemUser(token);

        if (systemUser is null) return Result.Fail("Пользователь не найден");

        return Result.Success();
    }

    public DataResult<UserToken?> LogIn(String? email, String? password)
    {
        if (String.IsNullOrWhiteSpace(email))
            return DataResult<UserToken?>.Failed("Не введен Email");

        if (String.IsNullOrWhiteSpace(password))
            return DataResult<UserToken?>.Failed("Не введен пароль");

        User? user = GetUser(email, HashManager.DefinePasswordHash(password));

        if (user is null) return DataResult<UserToken?>.Failed("Пользователь не найден, проверьте правильность введенных данных");

        UserToken? userToken = UserToken.New(user.Id);

        Result savingUserTokenResult = SaveUserToken(userToken);

        if (!savingUserTokenResult.IsSuccess) return DataResult<UserToken?>.Failed(savingUserTokenResult.Errors[0].Message);

        Result authentificationResult = Authenticate(userToken.Token);

        if (!authentificationResult.IsSuccess) return DataResult<UserToken?>.Failed(authentificationResult.Errors[0].Message);

        return DataResult<UserToken?>.Success(userToken);
    }

    public void LogOut(String token) => _usersRepository.RemoveToken(token);
}
