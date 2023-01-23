using AAS.Services.Common;

namespace AAS.Services.Users.Repositories.Queries;
internal static class Sql
{
    #region Users

    public static String Users_Save => SqlFileProvider.GetQuery(folder: "Users");
    public static String Users_GetById => SqlFileProvider.GetQuery(folder: "Users");
    public static String Users_GetByName => SqlFileProvider.GetQuery(folder: "Users");
    public static String Users_DeleteByName => SqlFileProvider.GetQuery(folder: "Users");


    #endregion
}
