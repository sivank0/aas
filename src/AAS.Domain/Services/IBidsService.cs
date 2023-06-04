#region

using AAS.Domain.Bids;
using AAS.Domain.Bids.Enums;
using AAS.Domain.Users.SystemUsers;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

#endregion

namespace AAS.Domain.Services;

public interface IBidsService
{
    Task<Result> SaveBid(BidBlank bidBlank, SystemUser systenUser);
    Bid? GetBid(ID id);
    Bid[] GetAllBids();
    Bid[] GetBySearch(string searchableText);
    Result ChangeBidDenyDescription(ID bidId, String? bidDenyDescription = null, Boolean canBeBidDenyDescriptionNull = false);
    Result ChangeBidStatus(ID bidId, BidStatus bidStatus);
    Result RemoveBid(ID bidId, ID systemUserId);
}