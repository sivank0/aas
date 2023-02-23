using AAS.Domain.Bids.Enums;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Bids.Models;

public class BidDb
{
    public ID Id { get; set; }
    public Int32 Number { get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public String? DenyDescription { get; set; }
    public BidStatus Status { get; set; }
    public DateTime? AcceptanceDate { get; set; }
    public DateTime? ApproximateDate { get; set; }

    public ID CreatedUserId { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }

    public ID? ModifiedUserId { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }

    public BidDb(ID id, Int32 number, string title, string description, string? denyDescription, BidStatus status, DateTime? acceptanceDate, DateTime? approximateDate, ID createdUserId, DateTime createdDateTimeUtc, ID? modifiedUserId, DateTime? modifiedDateTimeUtc, bool isRemoved)
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
