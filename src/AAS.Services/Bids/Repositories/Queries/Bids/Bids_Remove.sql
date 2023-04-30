UPDATE bids
SET isremoved           = true,
    modifieduserid      = @p_systemuserid,
    modifieddatetimeutc = @p_currentdatetimeutc
WHERE id = @p_bidid