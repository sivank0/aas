SELECT *
FROM bids
WHERE (title ILIKE '%' || @p_searchableText || '%') OR (description ILIKE '%' || @p_searchableText || '%')
