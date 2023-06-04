#region

using AAS.Domain.Bids;
using AAS.Domain.Bids.Enums;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

#endregion

namespace AAS.Services.Bids.Repositories;

public interface IBidsRepository
{
    public void SaveBid(BidBlank bidBlank,String[] bidFilePaths, ID systemUserId);
    public Bid? GetBid(ID id);
    public Bid[] GetAllBids();
    public Bid[] GetBySearch(string searchableText);
    public int GetBidsMaxNumber();
    public void ChangeBidDenyDescription(ID bidId, String bidDenyDescription);
    public void ChangeBidStatus(ID bidId, BidStatus bidStatus);
    public void RemoveBid(ID bidId, ID systemUserId);
}