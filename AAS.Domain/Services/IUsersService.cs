using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
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
    Result RegisterUser(UserRegistrationBlank userRegistrationBlank);
    User? GetUser(ID id);
    User? GetUser(String email, String? password = null);
    User[] GetUsers();
    Result RemoveUser(ID userId, ID systemUserId);

    #endregion

    #region Roles
    UserRole GetUserRole(ID userId);
    #endregion
}
