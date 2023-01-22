using AAS.Domain.Bids.Enums;

namespace AAS.Domain.Bids;

public class Bid
{
    public Guid Id { get; }
    public string Title { get; }
    public string Description { get; }
    public string DenyDescription { get; }
    public BidStatus Status { get; }
    public DateOnly AcceptanceDate { get; }
    public DateOnly ApproximateDate { get; }
    public Guid CreatedUserId { get; }

    public Bid(Guid id, string title, string description, string denyDescription, BidStatus status, DateOnly acceptanceDate, DateOnly approximateDate, Guid createdUserId)
    {
        Id = id;
        Title = title;
        Description = description;
        DenyDescription = denyDescription;
        Status = status;
        AcceptanceDate = acceptanceDate;
        ApproximateDate = approximateDate;
        CreatedUserId = createdUserId;
    }
}   
