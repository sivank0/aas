using AAS.Domain.Bids.Enums;

namespace AAS.Domain.Bids;

public class BidBlank
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? DenyDescription { get; set; }
    public BidStatus? Status { get; set; }
    public DateOnly? AcceptanceDate { get; set; }
    public DateOnly? ApproximateDate { get; set; }
    public Guid? CreatedUserId { get; set; }
}   
