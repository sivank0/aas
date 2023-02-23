using AAS.Domain.Bids;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Domain.Services;

public interface IBidsService
{
    Result SaveBid(BidBlank bidBlank, ID systenUserId);
    PagedResult<Bid> GetPagedBids(Int32 page, Int32 count);
}
