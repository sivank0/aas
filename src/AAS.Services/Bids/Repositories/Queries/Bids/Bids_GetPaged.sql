SELECT *, COUNT(*) OVER()
FROM bids
WHERE NOT isremoved
ORDER BY number
OFFSET @p_offset LIMIT @p_limit;