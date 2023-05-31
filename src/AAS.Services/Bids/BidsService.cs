#region

using AAS.Domain.AccessPolicies;
using AAS.Domain.Bids;
using AAS.Domain.Bids.Enums;
using AAS.Domain.Files;
using AAS.Domain.Files.Enums;
using AAS.Domain.Services;
using AAS.Domain.Users.SystemUsers;
using AAS.Services.Bids.Repositories;
using AAS.Tools.Types.Files;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

#endregion

namespace AAS.Services.Bids;

public class BidsService : IBidsService
{
    private readonly IBidsRepository _bidsRepository;
    private readonly IFileStorageService _fileStorageService;

    public BidsService(IBidsRepository bidsRepository, IFileStorageService fileStorageService)
    {
        _bidsRepository = bidsRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result> SaveBid(BidBlank bidBlank, SystemUser systemUser)
    {
        if (!systemUser.HasAccess(AccessPolicy.BidsUpdate) && bidBlank.CreatedUserId != systemUser.Id)
            return Result.Fail("Вы не имеете доступ к редактированию этой заявки");

        if (String.IsNullOrWhiteSpace(bidBlank.Title))
            return Result.Fail("Вы не ввели заголовок заявки");

        if (bidBlank.Status == BidStatus.Denied && string.IsNullOrWhiteSpace(bidBlank.DenyDescription))
            return Result.Fail("Вы не ввели причину отказа");

        bidBlank.Id ??= ID.New();
        bidBlank.Number ??= GetBidsMaxNumber() + 1;

        List<String> bidFilePaths = bidBlank.FileBlanks.Where(fileBlank => fileBlank.State == FileState.Intact).Select(fileBlank => fileBlank.Path!).ToList();

        if (bidBlank.FileBlanks.Length != 0)
        {
            (FileDetailsOfBase64[] fileDetailsOfBytes, String[] removeFilePaths) =
                FileBlank.GetBidFileDetails(bidBlank.Id.Value, bidBlank.FileBlanks);

            Result result = await _fileStorageService.SaveAndRemoveFiles(fileDetailsOfBytes, removeFilePaths);

            if (!result.IsSuccess)
                return Result.Fail(result.Errors[0]);

            bidFilePaths.AddRange(fileDetailsOfBytes.Select(fileDetails => fileDetails.FullPath!));
        }

        _bidsRepository.SaveBid(bidBlank, bidFilePaths.ToArray(), systemUser.Id);

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