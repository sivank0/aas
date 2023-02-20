using AAS.Domain.Bids;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Services.Bids.Repositories;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Services.Bids;

public class BidsService : IBidsService
{
    private readonly IBidsRepository _bidsRepository;

    public BidsService(IBidsRepository bidsRepository)
    {
        _bidsRepository = bidsRepository;
    }
    public Result SaveBid(BidBlank bidBlank, ID systemUserId)
    {
        bidBlank.Id ??= ID.New();
        _bidsRepository.SaveBid(bidBlank, systemUserId);
        return Result.Success();
    }

    public Bid[] GetBids()
    {
        return _bidsRepository.GetBids();
    }
}
