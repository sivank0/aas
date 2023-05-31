#region

using AAS.Domain.Bids.Enums;
using AAS.Domain.Files;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Domain.Bids;

public class BidBlank
{
    public ID? Id { get; set; }
    public int? Number { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? DenyDescription { get; set; }
    public FileBlank[] FileBlanks { get; set; }
    public BidStatus Status { get; set; }
    public DateOnly? ApproximateDate { get; set; }
    public ID CreatedUserId { get; set; }

    public BidBlank(ID? id, int? number, string? title, string? description, string? denyDescription, FileBlank[] fileBlanks, BidStatus status,
        DateOnly? approximateDate, ID createdUserId)
    {
        Id = id;
        Number = number;
        Title = title;
        Description = description;
        DenyDescription = denyDescription;
        FileBlanks = fileBlanks;
        Status = status;
        ApproximateDate = approximateDate;
        CreatedUserId = createdUserId;
    }
}