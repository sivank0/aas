#region

using AAS.Domain.Bids.Enums;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Services.Bids.Models;

public class BidDb
{
    public ID Id { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? DenyDescription { get; set; }
    public BidStatus Status { get; set; }
    public DateTime? AcceptanceDate { get; set; }
    public DateTime? ApproximateDate { get; set; }

    public ID CreatedUserId { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }

    public ID? ModifiedUserId { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public bool IsRemoved { get; set; }

    public BidDb(ID id, int number, string title, string description, string? denyDescription, BidStatus status,
        DateTime? acceptanceDate, DateTime? approximateDate, ID createdUserId, DateTime createdDateTimeUtc,
        ID? modifiedUserId, DateTime? modifiedDateTimeUtc, bool isRemoved)
    {
        Id = id;
        Number = number;
        Title = title;
        Description = description;
        DenyDescription = denyDescription;
        Status = status;
        AcceptanceDate = acceptanceDate;
        ApproximateDate = approximateDate;
        CreatedUserId = createdUserId;
        CreatedDateTimeUtc = createdDateTimeUtc;
        ModifiedUserId = modifiedUserId;
        ModifiedDateTimeUtc = modifiedDateTimeUtc;
        IsRemoved = isRemoved;
    }
}