#region

using AAS.Domain.AccessPolicies.Extensions;
using AAS.Domain.Users;
using AAS.Domain.Users.Permissions;
using AAS.Domain.Users.Roles;
using AAS.Services.Users.Converters;
using AAS.Services.Users.Models;
using AAS.Services.Users.Repositories.Queries;
using AAS.Tools.DB;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Services.Users.Repositories;

public partial class UsersRepository : NpgSqlRepository, IUsersRepository
{
    public UsersRepository(string connectionString) : base(connectionString)
    {
    }

    #region Users

    public void SaveUser(UserBlank userBlank, ID systemUserId)
    {
        String? userPhotoPath = userBlank.FileBlank is null
            ? null
            : String.IsNullOrWhiteSpace(userBlank.FileBlank.Path)
                ? null
                : userBlank.FileBlank.Path;

        SqlParameter[] saveUserParameters =
        {
            new("p_id", userBlank.Id!.Value),
            new("p_firstname", userBlank.FirstName!),
            new("p_middlename", userBlank.MiddleName!),
            new("p_lastname", userBlank.LastName!),
            new("p_email", userBlank.Email!),
            new("p_phonenumber", userBlank.PhoneNumber!),
            new("p_passwordhash", userBlank.Passwordhash ?? ""),
            new("p_userphotopath", userPhotoPath),
            new("p_systemuserid", systemUserId),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        SqlParameter[] saveUserPermissionParameters =
        {
            new("p_userid", userBlank.Id!.Value),
            new("p_userroleid", userBlank.RoleId!.Value)
        };

        ExecutionInTransaction(command =>
        {
            command.Execute(Sql.Users_Save, saveUserParameters);
            command.Execute(Sql.UserPermissions_Save, saveUserPermissionParameters);
        });
    }

    public void RegisterUser(UserRegistrationBlank userRegistrationBlank)
    {
        String? userPhotoPath = userRegistrationBlank.FileBlank is null
            ? null
            : String.IsNullOrWhiteSpace(userRegistrationBlank.FileBlank.Path)
                ? null
                : userRegistrationBlank.FileBlank.Path;
        
        SqlParameter[] parameters =
        {
            new("p_id", userRegistrationBlank.Id!.Value),
            new("p_firstname", userRegistrationBlank.FirstName!),
            new("p_middlename", userRegistrationBlank.MiddleName!),
            new("p_lastname", userRegistrationBlank.LastName!),
            new("p_email", userRegistrationBlank.Email!),
            new("p_passwordhash", userRegistrationBlank.PasswordHash),
            new("p_phonenumber", userRegistrationBlank.PhoneNumber!),
            new("p_userphotopath", userPhotoPath),
            new("p_systemuserid", userRegistrationBlank.Id!),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.Users_Save, parameters);
    }

    public User? GetUser(ID userId)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userId)
        };

        return Get<UserDb?>(Sql.Users_GetById, parameters)?.ToUser();
    }

    public User? GetUser(string email, string? passwordHash)
    {
        SqlParameter[] parameters =
        {
            new("p_email", email),
            new("p_passwordHash", passwordHash)
        };

        return Get<UserDb?>(Sql.Users_GetByEmailAndPassword, parameters)?.ToUser();
    }

    public User[] GetUsers()
    {
        return GetArray<UserDb>(Sql.Users_GetAll).ToUsers();
    }

    public void ChangeUserPassword(ID userId, string passwordHash, ID systemUserId)
    {
        SqlParameter[] parameters =
        {
            new("p_userid", userId),
            new("p_passwordhash", passwordHash),
            new("p_systemuserid", systemUserId),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.Users_ChangePassword, parameters);
    }

    public void RemoveUser(ID userId, ID systemUserId)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userId),
            new("p_systemuserid", systemUserId),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.Users_Remove, parameters);
    }

    #endregion

    #region UserRoles

    public void SaveUserRole(UserRoleBlank userRoleBlank, ID systemUserId)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userRoleBlank.Id!.Value),
            new("p_name", userRoleBlank.Name!),
            new("p_accesspolicies", userRoleBlank.AccessPolicies.Keys()),
            new("p_systemuserid", systemUserId),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.UserRoles_Save, parameters);
    }

    public UserRole? GetUserRole(ID userRoleId)
    {
        SqlParameter[] parameters =
        {
            new("p_userRoleId", userRoleId)
        };

        return Get<UserRoleDb?>(Sql.UserRoles_GetByRoleId, parameters)?.ToUserRole();
    }

    public UserRole? GetUserRoleByUserId(ID userId)
    {
        SqlParameter[] parameters =
        {
            new("p_userId", userId)
        };

        return Get<UserRoleDb?>(Sql.UserRoles_GetByUserId, parameters)?.ToUserRole();
    }

    public UserRole[] GetUserRoles()
    {
        return GetArray<UserRoleDb>(Sql.UserRoles_GetAll).ToUserRoles();
    }

    public void RemoveUserRole(ID userRoleId, ID systemUserId)
    {
        SqlParameter[] parameters =
        {
            new("p_userroleid", userRoleId),
            new("p_systemuserid", systemUserId),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.UserRoles_Remove, parameters);
    }

    #endregion

    #region Permissions

    public UserPermission? GetUserPermission(ID userId)
    {
        SqlParameter[] parameters =
        {
            new("p_userId", userId)
        };

        return Get<UserPermissionDb?>(Sql.UserPermissions_GetByUserId, parameters)?.ToUserPermission();
    }

    #endregion
}