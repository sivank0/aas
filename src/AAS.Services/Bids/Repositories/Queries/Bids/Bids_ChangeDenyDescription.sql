UPDATE bids
SET denydescription = @p_biddenydescription
WHERE id = @p_bidid AND NOT isremoved