#region

using AAS.Domain.Bids;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

#endregion

namespace AAS.Domain.Services;

public interface IBidsService
{
    Result SaveBid(BidBlank bidBlank, ID systenUserId);
    Bid? GetBid(ID id);
    Bid[] GetAllBids();
    Result RemoveBid(ID bidId, ID systemUserId);
}