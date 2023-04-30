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
    PagedResult<Bid> GetPagedBids(int page, int count);
    Result RemoveBid(ID bidId, ID systemUserId);
}