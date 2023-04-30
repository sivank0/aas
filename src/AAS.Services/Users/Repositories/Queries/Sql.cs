#region

using AAS.Services.Common;

#endregion

namespace AAS.Services.Users.Repositories.Queries;

internal static class Sql
{
    #region Users

    public static string Users_Save => SqlFileProvider.GetQuery(folder: "Users");
    public static string Users_GetById => SqlFileProvider.GetQuery(folder: "Users");
    public static string Users_GetByEmailAndPassword => SqlFileProvider.GetQuery(folder: "Users");
    public static string Users_GetByEmailAndPass => SqlFileProvider.GetQuery(folder: "Users");
    public static string Users_GetAll => SqlFileProvider.GetQuery(folder: "Users");
    public static string Users_ChangePassword => SqlFileProvider.GetQuery(folder: "Users");
    public static string Users_Remove => SqlFileProvider.GetQuery(folder: "Users");

    #endregion

    #region UserRoles

    public static string UserRoles_Save => SqlFileProvider.GetQuery(folder: "Roles");
    public static string UserRoles_GetByRoleId => SqlFileProvider.GetQuery(folder: "Roles");
    public static string UserRoles_GetByUserId => SqlFileProvider.GetQuery(folder: "Roles");
    public static string UserRoles_GetAll => SqlFileProvider.GetQuery(folder: "Roles");
    public static string UserRoles_Remove => SqlFileProvider.GetQuery(folder: "Roles");

    #endregion

    #region Permissions

    public static string UserPermissions_Save => SqlFileProvider.GetQuery(folder: "Permissions");
    public static string UserPermissions_GetByUserId => SqlFileProvider.GetQuery(folder: "Permissions");

    #endregion

    #region UserTokens

    public static string UserTokens_Save => SqlFileProvider.GetQuery(folder: "Tokens");
    public static string UserTokens_GetByToken => SqlFileProvider.GetQuery(folder: "Tokens");
    public static string UserTokens_Remove => SqlFileProvider.GetQuery(folder: "Tokens");

    #endregion
}