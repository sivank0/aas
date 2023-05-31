#region

using AAS.Domain.EmailVerifications;
using AAS.Domain.Users;
using AAS.Domain.Users.Permissions;
using AAS.Domain.Users.Roles;
using AAS.Domain.Users.SystemUsers;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

#endregion

namespace AAS.Domain.Services;

public interface IUsersService
{
    #region Authentification

    Result Authenticate(string token);
    SystemUser? GetSystemUser(string token);
    DataResult<UserToken?> LogIn(string? email, string? password);
    void LogOut(string token);

    #endregion

    #region Users

    User? GetUser(ID userId, Boolean includeRemoved = false);
    User? GetUser(string email);
    (User user, EmailVerification emailVerification)? GetUserEmailVerification(ID userId);
    (User user, EmailVerification emailVerification)? GetUser(string email, string? passwordHash = null);
    (User user, EmailVerification emailVerification)? GetUserEmailVerification(string userEmailVerificationToken);
    User[] GetUsers();
    Result RemoveUser(ID userId, ID systemUserId);

    #endregion

    #region Roles

    Result SaveUserRole(UserRoleBlank userRoleBlank, ID systemUserId);
    UserRole? GetUserRole(ID userRoleId);
    UserRole? GetUserRoleByUserId(ID userId);
    UserRole[] GetUserRoles();
    Result RemoveUserRole(ID userRoleId, ID systemUserId);

    #endregion

    #region Permissions

    UserPermission? GetUserPermission(ID userId);

    #endregion

    #region Tokens

    Result SaveUserToken(UserToken userToken);
    UserToken? GetUserToken(string token);

    #endregion
}