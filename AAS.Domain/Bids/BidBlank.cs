using AAS.Domain.Bids.Enums;
using AAS.Tools.Types.IDs;

namespace AAS.Domain.Bids;

public class BidBlank
{
    public ID? Id { get; set; }
    public Int32? Number { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? DenyDescription { get; set; }
    public BidStatus Status { get; set; }
    public DateOnly? ApproximateDate { get; set; }

    public BidBlank(ID? id, Int32? number, string? title, string? description, string? denyDescription, BidStatus status, DateOnly? approximateDate)
    {
        Id = id;
        Number = number;
        Title = title;
        Description = description;
        DenyDescription = denyDescription;
        Status = status;
        ApproximateDate = approximateDate;
    }
}
