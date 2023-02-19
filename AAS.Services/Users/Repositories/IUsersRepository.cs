using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Repositories;

public interface IUsersRepository
{
    #region Users

    public void SaveUser(UserBlank userBlank);
    public void RegisterUser(UserRegistrationBlank userRegistrationBlank);
    public User? GetUser(ID id);
    public User? GetUser(String email, String? passwordHash = null);
    public User[] GetUsers();
    public void RemoveUser(ID userId, ID systemUserId);

    #endregion

    #region UserRoles

    public UserRole? GetUserRole(ID userId);

    #endregion

    #region UserTokens

    public void SaveUserToken(UserToken userToken);
    public UserToken? GetUserToken(String token);

    #endregion
}
