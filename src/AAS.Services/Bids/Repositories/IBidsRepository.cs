#region

using AAS.Domain.Bids;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

#endregion

namespace AAS.Services.Bids.Repositories;

public interface IBidsRepository
{
    public void SaveBid(BidBlank bidBlank, ID systemUserId);
    public Bid? GetBid(ID id);
    public PagedResult<Bid> GetPagedBids(int page, int countInPage);
    public int GetBidsMaxNumber();
    public void RemoveBid(ID bidId, ID systemUserId);
}