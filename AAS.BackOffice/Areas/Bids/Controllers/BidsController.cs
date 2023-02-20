using AAS.BackOffice.Filters;
using AAS.Domain.AccessPolicies;
using AAS.Domain.Services;
using AAS.Domain.Users.SystemUsers;
using AAS.Domain.Users;
using AAS.Services.Users;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAS.Domain.Bids;
using AAS.BackOffice.Controllers;

namespace AAS.BackOffice.Areas.Bids.Controllers;

public class BidsController : BaseController
{
    private readonly IBidsService _bidsService;

    public BidsController(IBidsService bidsService)
    {
        _bidsService = bidsService;
    }

    [HttpPost("bids/save")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public Result SaveBid([FromBody] BidBlank bidBlank)
    {
        return _bidsService.SaveBid(bidBlank, SystemUser.Id);
    }

    //[HttpGet("users/get_by_id")]
    //[IsAuthorized(AccessPolicy.UsersRead)]
    //public User? GetUser(ID id)
    //{
    //    return _bidsService.GetUser(id);
    //}

    [HttpGet("bids/get_all")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public Bid[] GetAllBids()
    {
        return _bidsService.GetBids();
    }
}
