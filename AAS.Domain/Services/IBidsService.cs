using AAS.Domain.Bids;
using AAS.Domain.Users;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAS.Domain.Services;

public interface IBidsService
{
    Result SaveBid(BidBlank bidBlank, ID systenUserId);
    Bid[] GetBids();
}
