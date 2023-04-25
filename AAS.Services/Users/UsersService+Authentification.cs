#region

using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Domain.Users.SystemUsers;
using AAS.Domain.Users.UserAccesses;
using AAS.Tools.Managers;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

#endregion

namespace AAS.Services.Users;

public partial class UsersService : IUsersService // + Authentification
{
    public SystemUser? GetSystemUser(string token)
    {
        UserToken? userToken = GetUserToken(token);

        if (userToken is null) return null;

        User? user = GetUser(userToken.UserId);

        if (user is null) return null;

        UserRole? userRole = GetUserRoleByUserId(user.Id);

        if (userRole is null) return null;

        UserAccess userAccess = new UserAccess(userRole.AccessPolicies);

        return new SystemUser(user, userAccess);
    }

    public Result Authenticate(string token)
    {
        SystemUser? systemUser = GetSystemUser(token);

        if (systemUser is null) return Result.Fail("Пользователь не найден");

        return Result.Success();
    }

    public DataResult<UserToken?> LogIn(string? email, string? password)
    {
        if (string.IsNullOrWhiteSpace(email))
            return DataResult<UserToken?>.Fail("Не введен Email");

        if (string.IsNullOrWhiteSpace(password))
            return DataResult<UserToken?>.Fail("Не введен пароль");

        User? user = GetUser(email, HashManager.DefinePasswordHash(password));

        if (user is null)
            return DataResult<UserToken?>.Fail("Пользователь не найден, проверьте правильность введенных данных");

        UserToken? userToken = UserToken.New(user.Id);

        Result savingUserTokenResult = SaveUserToken(userToken);

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

        User? existingUser = GetUser(userRegistrationBlank.Email);

        if (existingUser is not null) return DataResult<UserToken?>.Fail("Пользователь с такой почтой существует");

        userRegistrationBlank.Id = ID.New();
        _usersRepository.RegisterUser(userRegistrationBlank);

        UserToken? userToken = UserToken.New(userRegistrationBlank.Id!.Value);

        Result savingUserTokenResult = SaveUserToken(userToken);

        if (!savingUserTokenResult.IsSuccess)
            return DataResult<UserToken?>.Fail(savingUserTokenResult.Errors[0].Message);

        return DataResult<UserToken?>.Success(userToken);
    }

    public void LogOut(string token)
    {
        _usersRepository.RemoveToken(token);
    }
}