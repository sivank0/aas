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

    public Result SaveUser(UserBlank userBlank)
    {
        if (String.IsNullOrWhiteSpace(userBlank.Email)) return Result.Fail("Не введен Email");

        if (String.IsNullOrWhiteSpace(userBlank.FirstName)) return Result.Fail("Не введено имя");

        if (String.IsNullOrWhiteSpace(userBlank.LastName)) return Result.Fail("Не введена фамилия");

        if (String.IsNullOrWhiteSpace(userBlank.PhoneNumber)) return Result.Fail("Не введнен номер телефона");

        _usersRepository.SaveUser(userBlank);
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

        userRegistrationBlank.Id ??= ID.New();
        _usersRepository.RegisterUser(userRegistrationBlank);
        return Result.Success();
    }

    public UserViewBlank AuthorizationUser(UserAuthorizationBlank userAuthorizationBlank)
    {
        if (String.IsNullOrWhiteSpace(userAuthorizationBlank.Email)) return null;
        if (String.IsNullOrWhiteSpace(userAuthorizationBlank.Password)) return null;


        userAuthorizationBlank.Password = HashManager.Hash(userAuthorizationBlank.Password);        
        var user = _usersRepository.AuthorizeUser(userAuthorizationBlank);
        UserViewBlank userView = new UserViewBlank();
        userView.Email = user.Email;
        userView.FirstName = user.FirstName;
        userView.LastName = user.LastName;
        userView.PhoneNumber = user.PhoneNumber;
        if (!String.IsNullOrWhiteSpace(user.MiddleName)) userView.MiddleName = user.MiddleName;
        return userView;
    }

    public User? GetUser(ID id)
    {
        return _usersRepository.GetUser(id);
    }

    public User? GetUser(String email, String passwordHash)
    {
        return _usersRepository.GetUser(email, passwordHash);
    }

    public Result RemoveUser(ID userId)
    {
        _usersRepository.RemoveUser(userId);

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
