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
    DataResult<UserToken?> RegisterUser(UserRegistrationBlank userRegistrationBlank);
    void LogOut(String token);

    #endregion

    #region Users

    Result SaveUser(UserBlank userBlank, ID systenUserId);
    User? GetUser(ID id);
    User? GetUser(String email, String? password = null);
    User[] GetUsers();
    Result ChangeUserPassword(ID userId, String? password, String? rePassword, ID systemUserId);
    Result RemoveUser(ID userId, ID systemUserId);

    #endregion

    #region Roles

    Result SaveUserRole(UserRoleBlank userRoleBlank, ID systemUserId);
    UserRole? GetUserRole(ID userId);
    UserRole[] GetUserRoles();

    #endregion
}
