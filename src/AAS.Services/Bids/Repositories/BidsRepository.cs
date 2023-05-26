#region

using AAS.Domain.Bids;
using AAS.Domain.Bids.Enums;
using AAS.Services.Bids.Models;
using AAS.Services.Bids.Repositories.Converters;
using AAS.Services.Bids.Repositories.Queries;
using AAS.Tools.DB;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

#endregion

namespace AAS.Services.Bids.Repositories;

public class BidsRepository : NpgSqlRepository, IBidsRepository
{
    public BidsRepository(string connectionString) : base(connectionString)
    {
    }

    public void SaveBid(BidBlank bidBlank, String[] bidFilePaths, ID systemUserId)
    {
        DateOnly? acceptanceDate = bidBlank.Status != BidStatus.AwaitingVerification
            ? DateOnly.FromDateTime(DateTime.UtcNow)
            : null;

        SqlParameter[] parameters =
        {
            new("p_id", bidBlank.Id!.Value),
            new("p_number", bidBlank.Number!.Value),
            new("p_title", bidBlank.Title!),
            new("p_description", bidBlank.Description!),
            new("p_denydescription", bidBlank.DenyDescription!),
            new("p_acceptancedate", acceptanceDate),
            new("p_filepaths", bidFilePaths),
            new("p_approximatedate", bidBlank.ApproximateDate!),
            new("p_status", bidBlank.Status!),
            new("p_systemuserid", systemUserId),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.Bids_Save, parameters);
    }

    public Bid? GetBid(ID id)
    {
        SqlParameter[] parameters =
        {
            new("p_id", id)
        };
        return Get<BidDb?>(Sql.Bids_GetById, parameters)?.ToBid();
    }

    public Bid[] GetAllBids()
    {
        return GetArray<BidDb>(Sql.Bids_GetAll).ToBids();
    }

    public int GetBidsMaxNumber()
    {
        return Get<int>(Sql.Bids_GetMaxNumber);
    }

    public void ChangeBidDenyDescription(ID bidId, String bidDenyDescription)
    {
        SqlParameter[] parameters =
        {
            new("p_bidid", bidId),
            new("p_biddenydescription", bidDenyDescription)
        };
        
        Execute(Sql.Bids_ChangeDenyDescription, parameters);
    }
    
    public void ChangeBidStatus(ID bidId, BidStatus bidStatus)
    {
        SqlParameter[] parameters =
        {
            new("p_bidid", bidId),
            new("p_bidstatus", bidStatus)
        };
        
        Execute(Sql.Bids_ChangeStatus, parameters);
    }
    
    public void RemoveBid(ID bidId, ID systemUserId)
    {
        SqlParameter[] parameters =
        {
            new("p_bidid", bidId),
            new("p_systemuserid", systemUserId),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.Bids_Remove, parameters);
    }
}