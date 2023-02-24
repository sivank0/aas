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

    #region UserRoles

    public Result SaveUserRole(UserRoleBlank userRoleBlank, ID systemUserId)
    {
        if (String.IsNullOrWhiteSpace(userRoleBlank.Name))
            return Result.Fail("Не введено название роли");

        if (userRoleBlank.AccessPolicies.Length == 0)
            return Result.Fail("Не выбраны политики доступа");

        userRoleBlank.Id ??= ID.New();

        _usersRepository.SaveUserRole(userRoleBlank, systemUserId);

        return Result.Success();
    }

    public UserRole? GetUserRole(ID userId)
    {
        return _usersRepository.GetUserRole(userId);
    }

    public UserRole[] GetUserRoles()
    {
        return _usersRepository.GetUserRoles();
    }

    #endregion
}
