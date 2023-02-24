using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Services.Users.Repositories;

public interface IUsersRepository
{
    #region Users

    public void SaveUser(UserBlank userBlank, ID systemUserId);
    public void RegisterUser(UserRegistrationBlank userRegistrationBlank);
    public User? GetUser(ID id);
    public User? GetUser(String email, String? passwordHash = null);
    public User[] GetUsers();
    public void ChangeUserPassword(ID userId, String passwordHash, ID systemUserId);
    public void RemoveUser(ID userId, ID systemUserId);

    #endregion

    #region UserRoles

    public void SaveUserRole(UserRoleBlank userRoleBlank, ID systemUserId);
    public UserRole? GetUserRole(ID userId);
    public UserRole[] GetUserRoles();

    #endregion

    #region UserTokens

    public void SaveUserToken(UserToken userToken);
    public UserToken? GetUserToken(String token);
    public void RemoveToken(String token);

    #endregion
}
