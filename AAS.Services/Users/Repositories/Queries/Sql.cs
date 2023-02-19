using AAS.Domain.Users.Roles;
using AAS.Services.Common;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Repositories.Queries;
internal static class Sql
{
    #region Users

    public static String Users_Save => SqlFileProvider.GetQuery(folder: "Users");
    public static String Users_GetById => SqlFileProvider.GetQuery(folder: "Users");
    public static String Users_GetByEmailAndPassword => SqlFileProvider.GetQuery(folder: "Users");
    public static String Users_GetByEmailAndPass => SqlFileProvider.GetQuery(folder: "Users");
    public static String Users_GetAll => SqlFileProvider.GetQuery(folder: "Users");
    public static String Users_Remove => SqlFileProvider.GetQuery(folder: "Users");

    #endregion

    #region UserRoles

    public static String UserRoles_GetByUserId => SqlFileProvider.GetQuery(folder: "Roles");

    #endregion

    #region UserTokens

    public static String UserTokens_Save => SqlFileProvider.GetQuery(folder: "Tokens");
    public static String UserTokens_GetByToken => SqlFileProvider.GetQuery(folder: "Tokens");

    #endregion
}
