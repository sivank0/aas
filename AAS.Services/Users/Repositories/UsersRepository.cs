using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Services.Users.Converters;
using AAS.Services.Users.Models;
using AAS.Services.Users.Repositories.Queries;
using AAS.Tools.DB;
using AAS.Tools.Managers;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Repositories;

public partial class UsersRepository : NpgSqlRepository, IUsersRepository
{
    public UsersRepository(String connectionString) : base(connectionString) { }

    #region Users

    //public void SaveUserAccess(UserAccessBlank userAccessBlank, ID userId)
    //{
    //    ExecutionInTransaction(command =>
    //    {

    //        UserAccessDb accessDb = userAccessBlank.ToUserAccessDb(userId);
    //        accessDb.ModifiedDateTimeUtc = accessDb.CreatedDateTimeUtc = DateTime.UtcNow;
    //        accessDb.ModifiedUserId = accessDb.CreatedUserId = userId;

    //        command.Execute(Sql.UserAccesses_Save, accessDb);

    //    });
    //}
    public void SaveUser(UserBlank userBlank)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userBlank.Id!),
            new("p_firstname", userBlank.FirstName!),
            new("p_middlename", userBlank.MiddleName!),
            new("p_lastname", userBlank.LastName!),
            new("p_email", userBlank.Email!),
            new("p_phonenumber", userBlank.PhoneNumber!),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.Users_Save, parameters);
    }

    public void RegisterUser(UserRegistrationBlank userRegistrationBlank)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userRegistrationBlank.Id!),
            new("p_firstname", userRegistrationBlank.FirstName!),
            new("p_middlename", userRegistrationBlank.MiddleName!),
            new("p_lastname", userRegistrationBlank.LastName!),
            new("p_email", userRegistrationBlank.Email!),
            new("p_passwordhash", HashManager.Hash(userRegistrationBlank.Password!)),
            new("p_phonenumber", userRegistrationBlank.PhoneNumber!),
            new("p_systemuserid", userRegistrationBlank.Id!),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.Users_Save, parameters);
    }

    public User? AuthorizeUser(UserAuthorizationBlank userAuthorizationBlank)
    {
        SqlParameter[] parameters =
        {
            new("p_email", userAuthorizationBlank.Email!),
            new("p_pass", userAuthorizationBlank.Password!),
        };

        return Get<UserDb?>(Sql.Users_GetByEmailAndPass, parameters)?.ToUser();
    }

    public User? GetUser(ID userId)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userId),
        };

        return Get<UserDb?>(Sql.Users_GetById, parameters)?.ToUser();
    }

    public User? GetUser(String email, String password)
    {
        SqlParameter[] parameters =
        {
            new("p_email", email),
            new("p_email", password)
        };

        return Get<UserDb?>(Sql.Users_GetByEmailAndPassword, parameters)?.ToUser();
    }

    public void RemoveUser(ID userId)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userId),
        };

        Execute(Sql.Users_DeleteByName, parameters);
    }

    #endregion

    #region UserRoles

    public UserRole? GetUserRole(ID userId)
    {
        SqlParameter[] parameters =
        {
            new("p_userId", userId)
        };

        return Get<UserRoleDb?>(Sql.UserRoles_GetByUserId, parameters)?.ToUserRole();
    }

    #endregion
}
