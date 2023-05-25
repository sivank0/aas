#region

using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.Domain.AccessPolicies;
using AAS.Domain.Bids;
using AAS.Domain.Bids.Enums;
using AAS.Domain.Services;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace AAS.BackOffice.Areas.Bids.Controllers;

public class BidsController : BaseController
{
    private readonly IBidsService _bidsService;

    public BidsController(IBidsService bidsService)
    {
        _bidsService = bidsService;
    }

    [HttpPost("bids/save")]
    [IsAuthorized(AccessPolicy.BidsUpdate)]
    public Result SaveBid([FromBody] BidBlank bidBlank)
    {
        return _bidsService.SaveBid(bidBlank, SystemUser.Id);
    }

    [HttpGet("bids/get_by_id")]
    [IsAuthorized(AccessPolicy.BidsRead)]
    public Bid? GetBid(ID bidId)
    {
        return _bidsService.GetBid(bidId);
    }

    [HttpGet("bids/get_all")]
    [IsAuthorized(AccessPolicy.BidsRead)]
    public Bid[] GetBidsAll()
    {
        return _bidsService.GetAllBids();
    }

    [HttpGet("bids/change_bid_deny_description")]
    [IsAuthorized(AccessPolicy.BidsUpdate)]
    public Result ChangeBidStatus(ID bidId, String? bidDenyDescription)
    {
        return _bidsService.ChangeBidDenyDescription(bidId, bidDenyDescription);
    }

    [HttpGet("bids/change_bid_status")]
    [IsAuthorized(AccessPolicy.BidsUpdate)]
    public Result ChangeBidStatus(ID bidId, BidStatus bidStatus)
    {
        return _bidsService.ChangeBidStatus(bidId, bidStatus);
    }

    [HttpGet("bids/remove")]
    [IsAuthorized(AccessPolicy.BidsUpdate)]
    public Result RemoveBid(ID bidId)
    {
        return _bidsService.RemoveBid(bidId, SystemUser.Id);
    }
}