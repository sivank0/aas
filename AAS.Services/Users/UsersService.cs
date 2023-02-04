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
        throw new NotImplementedException();
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
