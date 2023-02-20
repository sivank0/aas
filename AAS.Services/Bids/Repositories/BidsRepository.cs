using AAS.Domain.Bids;
using AAS.Domain.Users;
using AAS.Services.Bids.Models;
using AAS.Services.Bids.Repositories.Converters;
using AAS.Services.Bids.Repositories.Queries;
using AAS.Tools.DB;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Bids.Repositories;

public class BidsRepository : NpgSqlRepository, IBidsRepository
{
    public BidsRepository(String connectionString) : base(connectionString) { }

    public void SaveBid(BidBlank bidBlank, ID systemUserId)
    {
        SqlParameter[] parameters =
        {
            new("p_id", bidBlank.Id!),
            new("p_title", bidBlank.Title!),
            new("p_description", bidBlank.Description!),
            new("p_denydescription", bidBlank.DenyDescription!),
            new("p_acceptamcedate", bidBlank.AcceptanceDate!),
            new("p_approximatedate", bidBlank.ApproximateDate!),
            new("p_status", bidBlank.Status!),
            new("p_systemuserid", systemUserId),
            new("p_currentdatetimeutc", DateTime.UtcNow)

        };

        Execute(Sql.Bids_Save, parameters);
    }
    public Bid[] GetBids()
    {
        return GetArray<BidDb>(Sql.Bids_GetAll).ToBids();
    }
}
