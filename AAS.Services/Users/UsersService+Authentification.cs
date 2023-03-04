using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Domain.Users.SystemUsers;
using AAS.Domain.Users.UserAccesses;
using AAS.Tools.Managers;
using AAS.Tools.Types.IDs;
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

        UserRole? userRole = GetUserRoleByUserId(user.Id);

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
            return DataResult<UserToken?>.Fail("Не введен Email");

        if (String.IsNullOrWhiteSpace(password))
            return DataResult<UserToken?>.Fail("Не введен пароль");

        User? user = GetUser(email, HashManager.DefinePasswordHash(password));

        if (user is null) return DataResult<UserToken?>.Fail("Пользователь не найден, проверьте правильность введенных данных");

        UserToken? userToken = UserToken.New(user.Id);

        Result savingUserTokenResult = SaveUserToken(userToken);

        if (!savingUserTokenResult.IsSuccess) return DataResult<UserToken?>.Fail(savingUserTokenResult.Errors[0].Message);

        Result authentificationResult = Authenticate(userToken.Token);

        if (!authentificationResult.IsSuccess) return DataResult<UserToken?>.Fail(authentificationResult.Errors[0].Message);

        return DataResult<UserToken?>.Success(userToken);
    }

    public DataResult<UserToken?> RegisterUser(UserRegistrationBlank userRegistrationBlank)
    {
        if (String.IsNullOrWhiteSpace(userRegistrationBlank.Email))
            return DataResult<UserToken?>.Fail("Не введен Email");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.FirstName))
            return DataResult<UserToken?>.Fail("Не введено имя");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.LastName))
            return DataResult<UserToken?>.Fail("Не введена фамилия");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.PhoneNumber))
            return DataResult<UserToken?>.Fail("Не введнен номер телефона");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.Password))
            return DataResult<UserToken?>.Fail("Не введнен пароль");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.RePassword))
            return DataResult<UserToken?>.Fail("Не введнен повторно пароль");

        if (userRegistrationBlank.Password != userRegistrationBlank.RePassword)
            return DataResult<UserToken?>.Fail("Пароли не совпадают");

        User? existingUser = GetUser(userRegistrationBlank.Email);

        if (existingUser is not null) return DataResult<UserToken?>.Fail("Пользователь с такой почтой существует");

        userRegistrationBlank.Id = ID.New();
        _usersRepository.RegisterUser(userRegistrationBlank);

        UserToken? userToken = UserToken.New(userRegistrationBlank.Id!.Value);

        Result savingUserTokenResult = SaveUserToken(userToken);

        if (!savingUserTokenResult.IsSuccess) return DataResult<UserToken?>.Fail(savingUserTokenResult.Errors[0].Message);

        return DataResult<UserToken?>.Success(userToken);
    }

    public void LogOut(String token) => _usersRepository.RemoveToken(token);
}
