using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Services.Users.Repositories;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

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
        throw new NotImplementedException();
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
