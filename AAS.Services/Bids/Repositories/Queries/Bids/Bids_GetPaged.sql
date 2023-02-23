SELECT *, COUNT(*) OVER() FROM bids
ORDER BY number
OFFSET @p_offset
LIMIT @p_limit;