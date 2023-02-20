using AAS.Domain.Bids;
using AAS.Domain.Users;
using AAS.Services.Bids.Models;
using AAS.Services.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAS.Services.Bids.Repositories.Converters;

public static class BidConverter
{
    public static Bid ToBid(this BidDb db)
    {
        return new Bid(db.Id, db.Title, db.Description, db.DenyDescription, db.Status, db.AcceptanceDate, db.ApproximateDate, db.CreatedUserId);
    }

    public static Bid[] ToBids(this BidDb[] dbs)
    {
        return dbs.Select(ToBid).ToArray();
    }
}
