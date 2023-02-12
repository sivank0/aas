using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Services.Users.Repositories;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Services.Users;
public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Result SaveUser(UserBlank userBlank)
    {
        if (String.IsNullOrWhiteSpace(userBlank.Email)) return Result.Fail("Не введен Email");

        if (String.IsNullOrWhiteSpace(userBlank.FirstName)) return Result.Fail("Не введено имя");

        if (String.IsNullOrWhiteSpace(userBlank.LastName)) return Result.Fail("Не введена фамилия");

        if (String.IsNullOrWhiteSpace(userBlank.PhoneNumber)) return Result.Fail("Не введнен номер телефона");

        if (String.IsNullOrWhiteSpace(userBlank.PasswordHash)) return Result.Fail("Не введен пароль");

        _usersRepository.SaveUser(userBlank);
        return Result.Success();
    }

    public User? GetUser(ID id)
    {
        return _usersRepository.GetUser(id);
    }

    public User? GetUser(String userName)
    {
        return _usersRepository.GetUser(userName);
    }

    public Result RemoveUser(ID userId)
    {
        _usersRepository.RemoveUser(userId);

        return Result.Success();
    }

}
