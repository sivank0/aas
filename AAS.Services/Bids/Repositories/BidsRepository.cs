﻿using AAS.Domain.Bids;
using AAS.Domain.Bids.Enums;
using AAS.Services.Bids.Models;
using AAS.Services.Bids.Repositories.Converters;
using AAS.Services.Bids.Repositories.Queries;
using AAS.Services.Users.Models;
using AAS.Tools.DB;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Services.Bids.Repositories;

public class BidsRepository : NpgSqlRepository, IBidsRepository
{
    public BidsRepository(String connectionString) : base(connectionString) { }

    public void SaveBid(BidBlank bidBlank, ID systemUserId)
    {
        DateOnly? acceptanceDate = bidBlank.Status != BidStatus.AwaitingVerification ? DateOnly.FromDateTime(DateTime.UtcNow) : null;

        SqlParameter[] parameters =
        {
            new("p_id", bidBlank.Id!.Value),
            new("p_number", bidBlank.Number!.Value),
            new("p_title", bidBlank.Title!),
            new("p_description", bidBlank.Description!),
            new("p_denydescription", bidBlank.DenyDescription!),
            new("p_acceptancedate", acceptanceDate),
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
            new("p_id", id),
        };
        return Get<BidDb?>(Sql.Bids_GetById, parameters).ToBid();
    }

    public PagedResult<Bid> GetPagedBids(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        SqlParameter[] parameters =
        {
            new("p_offset", offset),
            new("p_limit", limit)
        };

        return GetPageOver<BidDb>(Sql.Bids_GetPaged, parameters).ToPagedBids();
    }

    public Int32 GetBidsMaxNumber()
    {
        return Get<Int32>(Sql.Bids_GetMaxNumber);
    }
}
