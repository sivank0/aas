using AAS.Domain.Bids.Enums;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Bids.Models;

public class BidDb
{
    public ID Id { get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public String? DenyDescription { get; set; }
    public BidStatus Status { get; set; }
    public DateOnly? AcceptanceDate { get; set; }
    public DateOnly? ApproximateDate { get; set; }

    public ID CreatedUserId { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }

    public ID? ModifiedUserId { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }

    public BidDb(ID id, string title, string description, string? denyDescription, BidStatus status, DateOnly? acceptanceDate, DateOnly? approximateDate, ID createdUserId, DateTime createdDateTimeUtc, ID? modifiedUserId, DateTime? modifiedDateTimeUtc, bool isRemoved)
    {
        Id = id;
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
