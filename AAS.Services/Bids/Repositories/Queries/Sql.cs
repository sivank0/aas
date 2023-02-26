using AAS.Services.Common;

namespace AAS.Services.Bids.Repositories.Queries;

internal class Sql
{
    internal static String Bids_Save => SqlFileProvider.GetQuery(folder: "Bids");
    internal static String Bids_GetPaged => SqlFileProvider.GetQuery(folder: "Bids");
    internal static String Bids_GetMaxNumber => SqlFileProvider.GetQuery(folder: "Bids");
    internal static String Bids_GetById => SqlFileProvider.GetQuery(folder: "Bids");
}
