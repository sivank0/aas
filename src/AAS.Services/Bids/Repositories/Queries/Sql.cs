#region

using AAS.Services.Common;

#endregion

namespace AAS.Services.Bids.Repositories.Queries;

internal class Sql
{
    internal static string Bids_Save => SqlFileProvider.GetQuery(folder: "Bids");
    internal static string Bids_GetAll => SqlFileProvider.GetQuery(folder: "Bids");
    internal static string Bids_GetMaxNumber => SqlFileProvider.GetQuery(folder: "Bids");
    internal static string Bids_GetById => SqlFileProvider.GetQuery(folder: "Bids");
    internal static string Bids_Remove => SqlFileProvider.GetQuery(folder: "Bids");
}