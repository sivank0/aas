using AAS.Domain.Users;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Repositories;

public interface IUsersRepository
{
    public void SaveUser(UserBlank userBlank);
    public void RegisterUser(UserRegistrationBlank userRegistrationBlank);
    public User? GetUser(ID id);
    public User? AuthorizeUser(UserAuthorizationBlank userAuthorizationBlank);
    public User? GetUser(String userName);
    public void RemoveUser(ID userId);
}
