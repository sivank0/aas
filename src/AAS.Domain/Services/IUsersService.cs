#region

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
    DataResult<UserToken?> RegisterUser(UserRegistrationBlank userRegistrationBlank);
    void LogOut(string token);

    #endregion

    #region Users

    Task<Result> SaveUser(UserBlank userBlank, ID systenUserId);
    User? GetUser(ID id);
    User? GetUser(string email, string? password = null);
    User[] GetUsers();
    Result ChangeUserPassword(ID userId, string? password, string? rePassword, ID systemUserId);
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
}