#region

using AAS.Domain.Bids;
using AAS.Domain.Bids.Enums;
using AAS.Domain.Services;
using AAS.Services.Bids.Repositories;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

#endregion

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
        if (string.IsNullOrWhiteSpace(bidBlank.Title))
            return Result.Fail("Не введен заголовок заявки");

        if (bidBlank.Status == BidStatus.Denied && string.IsNullOrWhiteSpace(bidBlank.DenyDescription))
            return Result.Fail("Не введена причина отказа");

        bidBlank.Id ??= ID.New();
        bidBlank.Number ??= GetBidsMaxNumber() + 1;
        _bidsRepository.SaveBid(bidBlank, systemUserId);

        return Result.Success();
    }

    public Bid? GetBid(ID id)
    {
        return _bidsRepository.GetBid(id);
    }

    public Bid[] GetAllBids()
    {
        return _bidsRepository.GetAllBids();
    }

    public int GetBidsMaxNumber()
    {
        return _bidsRepository.GetBidsMaxNumber();
    }

    public Result ChangeBidDenyDescription(ID bidId, String? bidDenyDescription = null,
        Boolean canBeBidDenyDescriptionNull = false)
    {
        if (!canBeBidDenyDescriptionNull && String.IsNullOrWhiteSpace(bidDenyDescription))
            return Result.Fail("Необходимо ввести причину отказа по заявке");

        Bid? existingBid = GetBid(bidId);

        if (existingBid is null) return Result.Fail("Заявка не найдена");

        _bidsRepository.ChangeBidDenyDescription(bidId, bidDenyDescription);

        return Result.Success();
    }

    public Result ChangeBidStatus(ID bidId, BidStatus bidStatus)
    {
        Bid? existingBid = GetBid(bidId);

        if (existingBid is null) return Result.Fail("Заявка не найдена");

        if (bidStatus == BidStatus.Denied)
        {
            if (String.IsNullOrWhiteSpace(existingBid.DenyDescription))
                return Result.Fail("Необходимо указать причину отказа по заявке");
        }
        else ChangeBidDenyDescription(bidId, canBeBidDenyDescriptionNull: true);
        
        _bidsRepository.ChangeBidStatus(bidId, bidStatus);

        return Result.Success();
    }

    public Result RemoveBid(ID bidId, ID systemUserId)
    {
        _bidsRepository.RemoveBid(bidId, systemUserId);
        return Result.Success();
    }
}