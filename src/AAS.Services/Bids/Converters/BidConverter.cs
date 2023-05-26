using AAS.Domain.Bids;
using AAS.Services.Bids.Models;
using AAS.Tools.Types.Results;
using File = AAS.Domain.Files.File;

namespace AAS.Services.Bids.Repositories.Converters;

public static class BidConverter
{
    public static Bid ToBid(this BidDb db)
    {
        DateOnly? acceptanceDate = db.AcceptanceDate != null ? DateOnly.FromDateTime(db.AcceptanceDate.Value) : null;
        DateOnly? approximateDate = db.ApproximateDate != null ? DateOnly.FromDateTime(db.ApproximateDate.Value) : null;
        File[] files = db.FilePaths is null || db.FilePaths.Length == 0
            ? Array.Empty<File>()
            : db.FilePaths.Select(filePath => new File(filePath)).ToArray();

        return new Bid(db.Id, db.Number, db.Title, db.Description, db.DenyDescription, files, db.Status, acceptanceDate,
            approximateDate, db.CreatedUserId);
    }

    public static Bid[] ToBids(this BidDb[] dbs)
    {
        return dbs.Select(ToBid).ToArray();
    }

    public static PagedResult<Bid> ToPagedBids(this PagedResult<BidDb> pagedDbs)
    {
        return PagedResult.Create(pagedDbs.Values.Select(ToBid), pagedDbs.TotalRows);
    }
}