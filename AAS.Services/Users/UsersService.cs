using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Services.Users.Repositories;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using AAS.Tools.Managers;

namespace AAS.Services.Users;
public partial class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
     
    #region Users

    public Result SaveUser(UserBlank userBlank, ID systemUserId)
    {
        if (String.IsNullOrWhiteSpace(userBlank.Email)) return Result.Fail("Не введен Email");

        if (String.IsNullOrWhiteSpace(userBlank.FirstName)) return Result.Fail("Не введено имя");

        if (String.IsNullOrWhiteSpace(userBlank.LastName)) return Result.Fail("Не введена фамилия");

        if (String.IsNullOrWhiteSpace(userBlank.PhoneNumber)) return Result.Fail("Не введнен номер телефона");


        userBlank.Id ??= ID.New();
        _usersRepository.SaveUser(userBlank, systemUserId);
        return Result.Success();
    }

    public Result RegisterUser(UserRegistrationBlank userRegistrationBlank)
    {
        if (String.IsNullOrWhiteSpace(userRegistrationBlank.Email)) return Result.Fail("Не введен Email");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.FirstName)) return Result.Fail("Не введено имя");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.LastName)) return Result.Fail("Не введена фамилия");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.PhoneNumber)) return Result.Fail("Не введнен номер телефона");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.Password)) return Result.Fail("Не введнен пароль");

        if (String.IsNullOrWhiteSpace(userRegistrationBlank.RePassword)) return Result.Fail("Не введнен повторно пароль");

        if (userRegistrationBlank.Password != userRegistrationBlank.RePassword) return Result.Fail("Пароли не совпадают");

        User? existingUser = GetUser(userRegistrationBlank.Email);

        if (existingUser is not null) return Result.Fail("Пользователь с такой почтой существует");

        userRegistrationBlank.Id ??= ID.New();
        _usersRepository.RegisterUser(userRegistrationBlank);
        return Result.Success();
    }

    public User? GetUser(ID id)
    {
        return _usersRepository.GetUser(id);
    }

    public User? GetUser(String email, String? passwordHash = null)
    {
        return _usersRepository.GetUser(email, passwordHash);
    }

    public User[] GetUsers()
    {
        return _usersRepository.GetUsers();
    }

    public Result ChangeUserPassword(ID userId, String? password, String? rePassword, ID systemUserId)
    {
        User? user = GetUser(userId);

        if (user is null) return Result.Fail("Пользователь, которому меняется пароль не найден");

        if (String.IsNullOrWhiteSpace(password)) return Result.Fail("Не введен пароль");

        if (String.IsNullOrWhiteSpace(rePassword)) return Result.Fail("Не введен повтор пароля");

        if (password != rePassword) return Result.Fail("Пароли должны совпадать");

        String? passwordHash = HashManager.DefinePasswordHash(password);

        if (String.IsNullOrWhiteSpace(passwordHash)) return Result.Fail("Не удалось изменить пароль, повторите попытку ещё раз");

        _usersRepository.ChangeUserPassword(userId, passwordHash, systemUserId);

        return Result.Success();
    }

    public Result RemoveUser(ID userId, ID systemUserId)
    {
        _usersRepository.RemoveUser(userId, systemUserId);

        return Result.Success();
    }

    #endregion

    #region

    public UserRole? GetUserRole(ID userId)
    {
        return _usersRepository.GetUserRole(userId);
    }

    #endregion
}
