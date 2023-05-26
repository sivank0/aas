using AAS.Domain.Bids.Enums;
using AAS.Tools.Types.IDs;
using File = AAS.Domain.Files.File;

namespace AAS.Domain.Bids;

public class Bid
{
    public ID Id { get; }
    public int Number { get; }
    public string Title { get; }
    public string? Description { get; }
    public string? DenyDescription { get; }
    public File[] Files { get; }
    public BidStatus Status { get; }
    public DateOnly? AcceptanceDate { get; }
    public DateOnly? ApproximateDate { get; }
    public ID CreatedUserId { get; }

    public Bid(ID id, int number, string title, string? description, string? denyDescription, File[] files, BidStatus status,
        DateOnly? acceptanceDate, DateOnly? approximateDate, ID createdUserId)
    {
        Id = id;
        Number = number;
        Title = title;
        Description = description;
        DenyDescription = denyDescription;
        Files = files;
        Status = status;
        AcceptanceDate = acceptanceDate;
        ApproximateDate = approximateDate;
        CreatedUserId = createdUserId;
    }
}