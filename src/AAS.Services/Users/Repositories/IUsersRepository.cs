#region

using AAS.Domain.Users;
using AAS.Domain.Users.Permissions;
using AAS.Domain.Users.Roles;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Services.Users.Repositories;

public interface IUsersRepository
{
    #region Users

    public void SaveUser(UserBlank userBlank, ID systemUserId);
    public void RegisterUser(UserRegistrationBlank userRegistrationBlank);
    public User? GetUser(ID userId, Boolean includeRemoved = false);
    public User? GetUser(string email, string? passwordHash = null);
    public User[] GetUsers();
    public void ChangeUserPassword(ID userId, string passwordHash, ID systemUserId);
    public void RemoveUser(ID userId, ID systemUserId);

    #endregion

    #region UserRoles

    public void SaveUserRole(UserRoleBlank userRoleBlank, ID systemUserId);
    public UserRole? GetUserRole(ID userRoleId);
    public UserRole? GetUserRoleByUserId(ID userId);
    public UserRole[] GetUserRoles();
    public void RemoveUserRole(ID userRoleId, ID systemUserId);

    #endregion

    #region UserPermissions

    public UserPermission? GetUserPermission(ID userId);

    #endregion

    #region UserTokens

    public void SaveUserToken(UserToken userToken);
    public UserToken? GetUserToken(string token);
    public void RemoveToken(string token);

    #endregion
}