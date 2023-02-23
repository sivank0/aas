using AAS.Domain.Bids;
using AAS.Domain.Bids.Enums;
using AAS.Domain.Services;
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
        if (String.IsNullOrWhiteSpace(bidBlank.Title))
            return Result.Fail("Не введен заголовок заявки");

        if (bidBlank.Status == BidStatus.Denied && String.IsNullOrWhiteSpace(bidBlank.DenyDescription))
            return Result.Fail("Не введена причина отказа");

        bidBlank.Id ??= ID.New();
        bidBlank.Number ??= GetBidsMaxNumber() + 1;
        _bidsRepository.SaveBid(bidBlank, systemUserId);

        return Result.Success();
    }

    public PagedResult<Bid> GetPagedBids(Int32 page, Int32 count)
    {
        return _bidsRepository.GetPagedBids(page, count);
    }

    public Int32 GetBidsMaxNumber()
    {
        return _bidsRepository.GetBidsMaxNumber();
    }
}
