﻿using AAS.Domain.Bids.Enums;
using AAS.Tools.Types.IDs;

namespace AAS.Domain.Bids;

public class Bid
{
    public ID Id { get; }
    public string Title { get; }
    public string Description { get; }
    public string? DenyDescription { get; }
    public BidStatus Status { get; }
    public DateOnly? AcceptanceDate { get; }
    public DateOnly? ApproximateDate { get; }
    public ID CreatedUserId { get; }

    public Bid(ID id, string title, string description, string denyDescription, BidStatus status, DateOnly? acceptanceDate, DateOnly? approximateDate, ID createdUserId)
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
