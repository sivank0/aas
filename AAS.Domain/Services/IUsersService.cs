using AAS.Domain.Users;
using AAS.Domain.Users.SystemUsers;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Domain.Services;
public interface IUsersService
{
    #region Authentification

    Result Authenticate(String token);
    SystemUser? GetSystemUser(String token);
    DataResult<UserToken?> LogIn(String? email, String? password);

    #endregion

    #region Users

    Result SaveUser(UserBlank userBlank);
    User? GetUser(ID id);
    User? GetUser(String userName);
    Result RemoveUser(ID userId);

    #endregion
}
