using AAS.Domain.Bids;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Bids.Repositories;

public interface IBidsRepository
{
    public void SaveBid(BidBlank bidBlank, ID systemUserId);
    public Bid[] GetBids();
}
