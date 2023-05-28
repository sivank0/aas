#region

using AAS.Services.Common;

#endregion

namespace AAS.Services.EmailVerifications.Repositories.Queries;

internal static class Sql
{
    public static string EmailVerifications_Save => SqlFileProvider.GetQuery();
    public static string EmailVerifications_GetByUserId => SqlFileProvider.GetQuery();
}