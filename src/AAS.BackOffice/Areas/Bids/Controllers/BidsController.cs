﻿#region

using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.Domain.AccessPolicies;
using AAS.Domain.Bids;
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

    [HttpGet("bids/get_page")]
    [IsAuthorized(AccessPolicy.BidsRead)]
    public PagedResult<Bid> GetBidsPage(int page, int countInPage)
    {
        return _bidsService.GetPagedBids(page, countInPage);
    }

    [HttpGet("bids/remove")]
    [IsAuthorized(AccessPolicy.BidsUpdate)]
    public Result RemoveBid(ID bidId)
    {
        return _bidsService.RemoveBid(bidId, SystemUser.Id);
    }
}