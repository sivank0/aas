﻿using AAS.Domain.Bids;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Services.Bids.Repositories;

public interface IBidsRepository
{
    public void SaveBid(BidBlank bidBlank, ID systemUserId);
    public Bid? GetBid(ID id);
    public PagedResult<Bid> GetPagedBids(Int32 page, Int32 countInPage);
    public Int32 GetBidsMaxNumber();
    public void RemoveBid(ID bidId, ID systemUserId);
}