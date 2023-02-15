using AAS.Domain.Users;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Domain.Services;
public interface IUsersService
{
    Result SaveUser(UserBlank userBlank);
    Result RegisterUser(UserRegistrationBlank userRegistrationBlank);
    UserViewBlank AuthorizationUser(UserAuthorizationBlank userAuthorizationBlank);
    User? GetUser(ID id);
    User? GetUser(String userName);
    Result RemoveUser(ID userId);
}
