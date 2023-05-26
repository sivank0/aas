INSERT INTO bids(
    id,
    number,
    title,
    description,
    denydescription,
    status,
    filepaths,
    acceptancedate,
    approximatedate,
    createddatetimeutc,
    createduserid,
    isremoved
)
VALUES (
    @p_id,
    @p_number,
    @p_title,
    @p_description,
    @p_denydescription,
    @p_status,
    @p_filepaths,
    @p_acceptancedate,
    @p_approximatedate,
    @p_currentdatetimeutc,
    @p_systemuserid,
    false
)
ON CONFLICT (id) DO UPDATE SET
    title = @p_title,
    description = @p_description,
    denydescription = @p_denydescription,
    status = @p_status,
    filepaths = @p_filepaths,
    acceptancedate = @p_acceptancedate,
    approximatedate = @p_approximatedate,
    modifieddatetimeutc = @p_currentdatetimeutc,
    modifieduserid = @p_systemuserid
