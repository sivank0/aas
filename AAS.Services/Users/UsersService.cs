using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Services.Users.Repositories;
using PMS.Tools.Types.IDs;

namespace AAS.Services.Users;
public class UsersService : IUsersService
{
    private readonly UsersRepositry _usersRepository = new UsersRepositry();

    public string SaveUser(UserBlank userBlank)
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

    public User? DeleteUser(String userName)
    {
        return _usersRepository.DeleteUser(userName);
    }

}
