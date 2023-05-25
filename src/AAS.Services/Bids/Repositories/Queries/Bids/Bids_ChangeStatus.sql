UPDATE bids
SET status = @p_bidstatus
WHERE id = @p_bidid AND NOT isremoved